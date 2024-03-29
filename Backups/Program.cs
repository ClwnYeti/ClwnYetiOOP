﻿using System.IO;
using Backups.Classes;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            FileStream firstFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\1.txt");
            firstFile.Close();
            FileStream secondFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\2.txt");
            secondFile.Close();
            FileStream thirdFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\3.txt");
            thirdFile.Close();
            FileStream fourthFile = File.Create("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\JobObjects\\4.txt");
            fourthFile.Close();
            var backupJobBuilder = new BackupJobBuilder();
            backupJobBuilder.WithDirectory("C:\\Users\\crazy\\RiderProjects\\ClwnYeti\\Backups\\Backup");
            backupJobBuilder.WithArchiver(new SplitStoragesArchiver(new StorageRepositoryWithFileSystem()));
            BackupJob firstTest = backupJobBuilder.Create();
            backupJobBuilder.WithArchiver(new SingleStorageArchiver(new StorageRepositoryWithFileSystem()));
            BackupJob secondTest = backupJobBuilder.Create();
            firstTest.Add(firstFile.Name);
            firstTest.Add(secondFile.Name);
            firstTest.MakeRestorePoint();
            firstTest.Delete(firstFile.Name);
            firstTest.MakeRestorePoint();
            secondTest.Add(thirdFile.Name);
            secondTest.Add(fourthFile.Name);
            secondTest.MakeRestorePoint();
            secondTest.Add(firstFile.Name);
            secondTest.MakeRestorePoint();
        }
    }
}
