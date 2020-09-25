using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class PostTagRepository : BaseRepository, IPostTagRepository
    {
        public PostTagRepository(IConfiguration config) : base(config) { }

        public void AddPostTag(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())

                {
                    cmd.CommandText = @"
                        INSERT INTO PostTag(TagId, PostId)
                        OUTPUT INSERTED.ID
                        VALUES (@postId, @tagId)";

                    cmd.Parameters.AddWithValue("@tagId", postTag.TagId);
                    cmd.Parameters.AddWithValue("@postId", postTag.PostId);

                    postTag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}

