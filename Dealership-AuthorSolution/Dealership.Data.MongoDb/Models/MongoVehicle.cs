using Dealership.Common;
using Dealership.Data.Common;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;
using System.Collections.Generic;
using System.Text;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Dealership.Data.MongoDb.Models
{
    public abstract class MongoVehicle : IVehicle, ICommentable
    {
        private const string MakeProperty = "Make";
        private const string ModelProperty = "Model";
        private const string PriceProperty = "Price";
        private const string WheelsProperty = "Wheels";
        private const string CommentsHeader = "    --COMMENTS--";
        private const string NoCommentsHeader = "    --NO COMMENTS--";

        public MongoVehicle(string make, string model, decimal price, VehicleType type)
        {
            this.Make = make;
            this.Model = model;
            this.Price = price;
            this.Type = type;
            this.Wheels = (int)type;
            this.Comments = new List<IComment>();

            this.ValidateFields();
        }

        public VehicleType Type { get; set; }

        public int Wheels { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public IList<IComment> Comments { get; set; }

        public decimal Price { get; set; }
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format("{0}:", this.GetType().Name));
            builder.AppendLine(string.Format("  {0}: {1}", MakeProperty, this.Make));
            builder.AppendLine(string.Format("  {0}: {1}", ModelProperty, this.Model));
            builder.AppendLine(string.Format("  {0}: {1}", WheelsProperty, this.Wheels));
            builder.AppendLine(string.Format("  {0}: ${1}", PriceProperty, this.Price));
            builder.AppendLine(this.PrintAdditionalInfo());
            builder.AppendLine(this.PrintComments());
            return builder.ToString().TrimEnd();
        }

        protected abstract string PrintAdditionalInfo();

        private string PrintComments()
        {
            var builder = new StringBuilder();

            if (this.Comments.Count <= 0)
            {
                builder.AppendLine(string.Format("{0}", NoCommentsHeader));
            }
            else
            {
                builder.AppendLine(string.Format("{0}", CommentsHeader));

                foreach (var comment in this.Comments)
                {
                    builder.AppendLine(comment.ToString());
                }

                builder.AppendLine(string.Format("{0}", CommentsHeader));
            }

            return builder.ToString().TrimEnd();
        }

        private void ValidateFields()
        {
            Validator.ValidateIntRange(this.Wheels, Constants.MinWheels, Constants.MaxWheels, string.Format(Constants.NumberMustBeBetweenMinAndMax, WheelsProperty, Constants.MinWheels, Constants.MaxWheels));

            Validator.ValidateNull(this.Make, string.Format(Constants.PropertyCannotBeNull, MakeProperty));
            Validator.ValidateIntRange(this.Make.Length, Constants.MinMakeLength, Constants.MaxMakeLength, string.Format(Constants.StringMustBeBetweenMinAndMax, MakeProperty, Constants.MinMakeLength, Constants.MaxMakeLength));

            Validator.ValidateNull(this.Model, string.Format(Constants.PropertyCannotBeNull, ModelProperty));
            Validator.ValidateIntRange(this.Model.Length, Constants.MinModelLength, Constants.MaxModelLength, string.Format(Constants.StringMustBeBetweenMinAndMax, ModelProperty, Constants.MinModelLength, Constants.MaxModelLength));

            Validator.ValidateDecimalRange(this.Price, Constants.MinPrice, Constants.MaxPrice, string.Format(Constants.NumberMustBeBetweenMinAndMax, PriceProperty, Constants.MinPrice, Constants.MaxPrice));
        }
    }
}
