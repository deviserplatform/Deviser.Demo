using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deviser.Demo.Admin.Models
{
    public class Folder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Folder> SubFolders { get; set; }
        public virtual ICollection<FolderPermission> PagePermissions { get; set; }
        public Folder Parent { get; set; }
        public Guid ParentId { get; set; }
        public int SortOrder { get; set; }
    }

    public class FolderPermission
    {
        public Guid Id { get; set; }
        public Guid FolderId { get; set; }
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
    }
}
