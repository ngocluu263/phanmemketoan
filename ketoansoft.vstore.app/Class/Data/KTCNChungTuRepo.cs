﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ketoansoft.app.Class.Global;

namespace ketoansoft.app.Class.Data
{
    public class KTCNChungTuRepo
    {
        dbVstoreAppDataContext db = new dbVstoreAppDataContext(Const.builder.ConnectionString);

        public virtual KT_CNChungTu GetById(int id)
        {
            try
            {
                return this.db.KT_CNChungTus.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual IQueryable GetAll()
        {
            return this.db.KT_CNChungTus;
        }
        public virtual void Create(KT_CNChungTu user)
        {
            try
            {
                this.db.KT_CNChungTus.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(KT_CNChungTu user)
        {
            try
            {
                KT_CNChungTu userOld = this.GetById(user.ID);
                userOld = user;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(int id)
        {
            try
            {
                KT_CNChungTu user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(KT_CNChungTu user)
        {
            try
            {
                db.KT_CNChungTus.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(int id)
        {
            try
            {
                KT_CNChungTu user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(KT_CNChungTu user)
        {
            try
            {
                //user.IsDelete = true;
                db.SubmitChanges();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
