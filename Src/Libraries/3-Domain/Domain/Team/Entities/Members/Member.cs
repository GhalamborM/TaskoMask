﻿using TaskoMask.Domain.Core.Exceptions;
using TaskoMask.Domain.Core.Models;
using TaskoMask.Domain.Core.Services;
using TaskoMask.Domain.Core.ValueObjects;
using TaskoMask.Domain.Share.Resources;
using TaskoMask.Domain.Team.Members.Events;

namespace TaskoMask.Domain.Team.Entities.Members
{
    /// <summary>
    /// Members are those who manage their tasks in this system
    /// </summary>
    public class Member : BaseUser
    {
        #region Fields


        #endregion

        #region Ctors

        public Member(UserIdentity identity, UserAuthentication authentication)
            : base(identity, authentication)
        {
            AddDomainEvent(new MemberCreatedEvent(Id, identity.DisplayName.Value, identity.Email.Value, authentication.UserName.Value));
        }



        #endregion

        #region Properties


        #endregion

        #region Public Methods



        ///// <summary>
        ///// 
        ///// </summary>
        public void Update(UserDisplayName displayName, UserEmail email, UserPhoneNumber phoneNumber)
        {
            UpdateIdentity(displayName, email, phoneNumber);
            UpdateAuthenticationUserName(UserName.Create(email.Value));
            base.Update();

            AddDomainEvent(new MemberUpdatedEvent(Id, Identity.DisplayName.Value, Identity.Email.Value, Authentication.UserName.Value));
        }



        /// <summary>
        /// 
        /// </summary>
        public override void SetPassword(string password, IEncryptionService encryptionService)
        {
            base.SetPassword(password, encryptionService);
        }



        /// <summary>
        /// 
        /// </summary>
        public override void ChangePassword(string oldPassword, string newPassword, IEncryptionService encryptionService)
        {
            base.ChangePassword(oldPassword, newPassword, encryptionService);
        }



        /// <summary>
        /// 
        /// </summary>
        public override bool IsValidPassword(string password, IEncryptionService encryptionService)
        {
            return base.IsValidPassword(password, encryptionService);
        }




        /// <summary>
        /// 
        /// </summary>
        public override void SetIsActive(bool isActive)
        {
            base.SetIsActive(isActive);
        }



        /// <summary>
        /// 
        /// </summary>
        public override void SoftDelete()
        {
            base.SoftDelete();
        }



        /// <summary>
        /// 
        /// </summary>
        public override void Recycle()
        {
            base.Recycle();
        }



        #endregion

        #region Private Methods



        protected override void CheckInvariants()
        {
            if (!Identity.Email.Value.ToLower().Equals(Authentication.UserName.Value.ToLower()))
                throw new DomainException(DomainMessages.UserName_And_Email_Must_Be_The_Same);
        }



        #endregion
    }
}
