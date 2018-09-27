using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class EstruturaDAO
    {
        public static bool Save(Estrutura obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Estrutura.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Estrutura.Update(obj);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static List<Estrutura> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Estrutura.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Estrutura>();
            }

        }
        public static bool Remove(Estrutura obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Estrutura.Remove(obj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                ////
                return false;
            }

        }
    }
}
