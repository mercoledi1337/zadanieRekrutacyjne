using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.Core.Tags
{
    public interface ITagRepository
    {
        Task AddTag(Tag tag);
        Task<Tag> GetTagByName(string name);
        int GetPopulation();
        Task UpdateTag(Tag tag);
        public Task<List<Tag>> GetAllTagsDescByNames(int start, int skip);
        public Task<List<Tag>> GetAllTagsAscByNames(int start, int skip);
        public Task<List<Tag>> GetAllTagsDescByCount(int start, int skip);
        public Task<List<Tag>> GetAllTagsAscByCount(int start, int skip);
        
    }
}
