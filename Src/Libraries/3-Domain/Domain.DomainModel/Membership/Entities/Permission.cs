﻿using TaskoMask.Domain.Core.Models;


namespace TaskoMask.Domain.DomainModel.Membership.Entities
{
    public class Permission : BaseEntity
    {
        public string DisplayName { get; set; }
        public string SystemName { get; set; }
        public string GroupName { get; set; }

        #region Update private properties

        public void SetAsDeleted()
        {
            base.Delete();
            SetAsUpdated();
        }


        public void SetAsRecycled()
        {
            base.Recycle();
            SetAsUpdated();
        }

        public void SetAsUpdated()
        {
            base.UpdateModifiedDateTime();
        }

        #endregion

    }
}
