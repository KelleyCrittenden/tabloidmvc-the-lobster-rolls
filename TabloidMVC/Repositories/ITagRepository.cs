//Created By Kelley Crittenden
//This file holds methods

using System.Collections.Generic;
using TabloidMVC.Models;
using Microsoft.Data.SqlClient;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag GetTagById(int id);
        void AddTag(Tag tag);
        void DeleteTag(int id);
        void UpdateTag(Tag tag);
        List<Tag> GetDeletedTags();
        void ReinstateTag(int id);


    }
}
