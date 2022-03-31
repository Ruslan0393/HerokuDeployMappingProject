using Mapping.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mapping.Domain.Common
{
    public class BaseItem
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime EditedDate { get; set; }

        public Guid UpdatedBy { get; set; }

        public ItemState State { get; set; } = ItemState.created;

        public void Update(Guid userId)
        {
            EditedDate = DateTime.Now;
            UpdatedBy = userId;
            State = ItemState.updated;
        }
        public void Deleted(Guid userId)
        {
            EditedDate = DateTime.Now;
            UpdatedBy = userId;
            State = ItemState.deleted;
        }
    }
}
