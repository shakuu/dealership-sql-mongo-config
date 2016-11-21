using System.Text;

using Dealership.Data.Contracts;

namespace Dealership.Data.MongoDb.Models
{
    public class MongoComment : IComment
    {
        private const string CommentHeader = "    ----------";
        private const string ContentProperty = "Content";
        private const string CommentIndentation = "    ";
        private const string AuthorHeader = "      User: ";

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
    }
}
