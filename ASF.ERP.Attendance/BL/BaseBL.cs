using ASF.ERP.Attendance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ASF.ERP.Attendance.BL
{
    //public class BaseBL
    //{
    //    private Collaborators mUserLogged;
    //    private bool mIsSysAdmin;
    //    private string mUsername;
    //    private String mCookieUsuario;
    //    //private AuthUser mAuthUser;
    //    private string mServerPath;

    //    public BaseBL() {  }

    //    public BaseBL(String cookieUsuario)
    //    {
    //        mUsername = FormsAuthentication.Decrypt(cookieUsuario).Name;
    //        mUserLogged = this.GetUser(mUsername);
    //        mIsSysAdmin = new AuthUser(mUsername).IsSysAdmin;
    //        //mAuthUser = new AuthUser(mUsername);
    //        mCookieUsuario = cookieUsuario;
    //    }

    //    public BaseBL(String username, bool withUsername)
    //    {
    //        mUsername = username;
    //        mUserLogged = this.GetUser(mUsername);
    //        mIsSysAdmin = new AuthUser(mUsername).IsSysAdmin;
    //        //mAuthUser = new AuthUser(mUsername);
    //    }

    //    public BaseBL(String cookieUsuario, string serverPath)
    //    {
    //        mUsername = FormsAuthentication.Decrypt(cookieUsuario).Name;
    //        mUserLogged = this.GetUser(mUsername);
    //        mIsSysAdmin = new AuthUser(mUsername).IsSysAdmin;
    //        //mAuthUser = new AuthUser(mUsername);
    //        mCookieUsuario = cookieUsuario;
    //        mServerPath = serverPath;
    //    }

    //    public BaseBL(String username, bool withUsername, string serverPath)
    //    {
    //        mUsername = username;
    //        mUserLogged = this.GetUser(mUsername);
    //        mIsSysAdmin = new AuthUser(mUsername).IsSysAdmin;
    //        //mAuthUser = new AuthUser(mUsername);
    //        mServerPath = serverPath;
    //    }

    //    public Users GetUserLogged { get => mUserLogged; }
    //    public bool IsSysAdmin { get => mIsSysAdmin; }
    //    public string Username { get => mUsername; }
    //    public String CookieUsuario { get => mCookieUsuario; }
    //    //public AuthUser AuthUser { get => mAuthUser; }
    //    public string ServerPath { get => mServerPath; }

    //    public Users GetUser(String username)
    //    {
    //        using (ASF_APIEntities context = new ASF_APIEntities())
    //        {
    //            return context.Users
    //                .Include(s => s.Collaborator)
    //                .Where(u => u.Username == username)
    //                .FirstOrDefault();
    //        }
    //    }

    //    //public Users GetUser(int id)
    //    //{
    //    //    using (ASF_APIEntities context = new ASF_APIEntities())
    //    //    {
    //    //        return context.Users.Include(s => s.Collaborator).Where(u => u.UserId == id).FirstOrDefault();
    //    //    }
    //    //}

    //    //public Collaborators GetCollaborator(int id)
    //    //{
    //    //    using (ASF_APIEntities context = new ASF_APIEntities())
    //    //    {
    //    //        IList<Collaborators> collaborators = context.Collaborators.Where(u => u.CollaboratorId == id).ToList<Collaborators>();
    //    //        return collaborators[0];
    //    //    }
    //    //}
    //}
}