using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deviser.Admin.Config;
using Deviser.Admin.Services;
using Deviser.Demo.Admin.Data;
using Deviser.Demo.Admin.Models;
using Newtonsoft.Json;

namespace Deviser.Demo.Admin.Services
{
    public class FolderAdminService : IAdminTreeService<Folder>
    {
        public async Task<Folder> GetTree()
        {
            var folderTree = GetFolderTree();
            return await Task.FromResult(folderTree);
        }

        public async Task<Folder> GetItem(string itemId)
        {
            var folder = InMemoryDb.FlatFolders.First(f => f.Id == Guid.Parse(itemId));
            return await Task.FromResult(folder);
        }

        public async Task<IFormResult<Folder>> CreateItem(Folder item)
        {
            MapParentId(item);
            item.Id = Guid.NewGuid();
            InMemoryDb.FlatFolders.Add(item);
            var result = new FormResult<Folder>(item)
            {
                IsSucceeded = true,
                SuccessMessage = "Folder has been created"
            };
            return await Task.FromResult(result);
        }


        public async Task<IFormResult<Folder>> UpdateItem(Folder item)
        {
            var folder = InMemoryDb.FlatFolders.FirstOrDefault(e => e.Id == item.Id);
            if (folder == null)
                return await Task.FromResult(new FormResult<Folder>(null)
                {
                    IsSucceeded = false,
                    SuccessMessage = "Folder not found"
                });

            folder.Name = item.Name;
            var result = new FormResult<Folder>(folder)
            {
                IsSucceeded = true,
                SuccessMessage = "Folder has been updated"
            };
            return await Task.FromResult(result);
        }

        public async Task<IFormResult<Folder>> UpdateTree(Folder item)
        {
            UpdateFolderTree(item);
            var folderTree = GetFolderTree();
            var result = new FormResult<Folder>(folderTree)
            {
                IsSucceeded = true,
                SuccessMessage = "Folder tree has been updated"
            };
            return await Task.FromResult(result);
        }

        public async Task<IAdminResult<Folder>> DeleteItem(string itemId)
        {
            var folderToRemove = InMemoryDb.FlatFolders.FirstOrDefault(e => e.Id == Guid.Parse(itemId));
            if (!InMemoryDb.FlatFolders.Remove(folderToRemove))
            {
                return await Task.FromResult<AdminResult<Folder>>(new AdminResult<Folder>()
                {
                    IsSucceeded = false,
                    ErrorMessage = "Unable to delete Folder"
                });
            }

            var result = new FormResult<Folder>(folderToRemove)
            {
                IsSucceeded = true,
                SuccessMessage = "Folder has been deleted"
            };
            return await Task.FromResult(result);
        }

        private static Folder GetFolderTree()
        {
            var flatFoldersClone = JsonConvert.DeserializeObject<ICollection<Folder>>(JsonConvert.SerializeObject(InMemoryDb.FlatFolders));
            var rootFolder = flatFoldersClone.First(f => f.ParentId == Guid.Empty);
            GetFolderTreeRecursive(flatFoldersClone, rootFolder);
            return rootFolder;
        }

        private static void GetFolderTreeRecursive(ICollection<Folder> folders, Folder folder)
        {
            folder.SubFolders = folders
                .Where(p => p.ParentId == folder.Id)
                .OrderBy(p => p.SortOrder)
                .ToList();

            if (folder.SubFolders == null) return;

            foreach (var child in folder.SubFolders)
            {
                GetFolderTreeRecursive(folders, child);
            }
        }

        private void UpdateFolderTree(Folder folder)
        {
            folder.Parent = null;
            var dbFolder = InMemoryDb.FlatFolders.FirstOrDefault(f => f.Id == folder.Id);
            dbFolder.ParentId = folder.ParentId;
            dbFolder.SortOrder = folder.SortOrder;

            if (folder.SubFolders == null) return;

            foreach (var subFolder in folder.SubFolders)
            {
                subFolder.ParentId = folder.Id;
                UpdateFolderTree(subFolder);
            }
        }

        private static void MapParentId(Folder item)
        {
            if (item.Parent == null) return;
            item.ParentId = item.Parent.Id;
            item.Parent = null;
        }
    }
}