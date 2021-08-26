﻿using CourseProj.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProj.Data.interfaces
{
    public interface ICollections
    {
        IEnumerable<Collection> AllCollections { get; }
        IEnumerable<Collection> CollectionsForMainPage();
        void CreateCollection(Collection collection);
        public void DeleteCollection(int CollectionID);
        public void SaveDB();
        public IEnumerable<Collection> CollectionsByUserID(int uID);
        public Collection CollectByID(int ID);
        public void UpdateInfo(Collection collection);
    }
}
