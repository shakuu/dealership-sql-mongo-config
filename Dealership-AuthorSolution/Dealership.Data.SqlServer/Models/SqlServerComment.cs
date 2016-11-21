using System.Text;

using Dealership.Common;
using Dealership.Data.Common;
using Dealership.Data.Contracts;

namespace Dealership.Data.SqlServer.Models
{
    public class SqlServerComment : IComment
    {
        private const string CommentHeader = "    ----------";
        private const string ContentProperty = "Content";
        private const string CommentIndentation = "    ";
        private const string AuthorHeader = "      User: ";

        public SqlServerComment()
        {

        }

        public SqlServerComment(string content)
        {
            this.Content = content;

            this.ValidateFields();
        }
        
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format("{0}", CommentHeader));
            builder.AppendLine(CommentIndentation + this.Content);
            builder.AppendLine(AuthorHeader + this.Author);
            builder.Append(string.Format("{0}", CommentHeader));

            return builder.ToString();
        }

        private void ValidateFields()
        {
            Validator.ValidateNull(this.Content, string.Format(Constants.PropertyCannotBeNull, ContentProperty));
            Validator.ValidateIntRange(this.Content.Length, Constants.MinCommentLength, Constants.MaxCommentLength, string.Format(Constants.StringMustBeBetweenMinAndMax, ContentProperty, Constants.MinCommentLength, Constants.MaxCommentLength));
        }
    }
}
