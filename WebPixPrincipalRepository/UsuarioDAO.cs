using System;
using System.Collections.Generic;
using System.Linq;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class UsuarioDAO
    {
        public static Usuario Save(Usuario obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Usuario.Add(obj);
                        int Id = db.SaveChanges();
                        return db.Usuario.Where(x => x.ID == Id).FirstOrDefault();
                    }
                }
                else
                {
                    obj.DateAlteracao = DateTime.Now;
                    using (var db = new WebPixContext())
                    {
                        db.Usuario.Update(obj);
                        db.SaveChanges();
                        return new Usuario();
                    }
                }
            }
            catch (Exception e)
            {
                return new Usuario();
            }
        }
        public static List<Usuario> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Usuario.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Usuario>();
            }

        }
        public static bool Remove(Usuario obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Usuario.Remove(obj);
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
        public static Usuario GetById(int idCliente, int id)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var user = db.Usuario.Where(x => x.idCliente.Equals(idCliente) && x.ID.Equals(id)).SingleOrDefault();
                    return user;
                }
            }
            catch (Exception e)
            {
                return new Usuario();
            }
        }
    }
}
