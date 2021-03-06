﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Unreal_Class_Wizard.Model
{
    public class User
    {
        public UserInformation UserInformation { get; set; }

        internal static User LoadUser()
        {
            // this method will only load copyright infos for now...
            User user;

            XmlSerializer xs = new XmlSerializer(typeof(User));
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/User.xml", FileMode.OpenOrCreate);
            if(fs.Length > 0)
            {
                user = xs.Deserialize(fs) as User;
            }
            else
            {
                // Make new user
                user = new User();
                user.UserInformation = new UserInformation();
                user.UserInformation.CopyrightText = String.Format("Copyright 1998-{0} Epic Games, Inc. All Rights Reserved.", DateTime.Now.Year);

                // Save user
                xs.Serialize(fs, user);
            }
            return user;

        }

    }
}
