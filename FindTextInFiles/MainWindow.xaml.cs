﻿using System.Windows;
using IdealAutomate.Core;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Windows.Controls;
using System.Configuration;

namespace FindTextInFiles {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static string strPathToSearch = @"C:\SVNIA\trunk";

        public static string strSearchPattern = @"*.*";

        public static string strSearchExcludePattern = @"*.dll;*.exe;*.png;*.xml;*.cache;*.sln;*.suo;*.pdb;*.csproj;*.deploy";

        public static string strSearchText = @"notepad";

        public static string strLowerCaseSearchText = @"notepad";

        public static int intHits;

        public static bool boolMatchCase = false;

        public static bool boolUseRegularExpression = false;

        public static bool boolStringFoundInFile;

        public static List<MatchInfo> matchInfoList;
        public MainWindow() {

            bool boolRunningFromHome = false;
            var window = new Window() //make sure the window is invisible
            {
                Width = 0,
                Height = 0,
                Left = -2000,
                WindowStyle = WindowStyle.None,
                ShowInTaskbar = false,
                ShowActivated = false,
            };

            window.Show();
            IdealAutomate.Core.Methods myActions = new Methods();
            string strScriptName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string settingsDirectory = GetAppDirectoryForScript(strScriptName);
            string fileName;
            string strSavedDomainName;
            fileName = "DomainName.txt";
            strSavedDomainName = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
            if (strSavedDomainName == "") {
                strSavedDomainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            }
            InitializeComponent();
            this.Hide();
             strScriptName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

           
            DisplayFindTextInFilesWindow:
            int intRowCtr = 0;
            ControlEntity myControlEntity = new ControlEntity();
            List<ControlEntity> myListControlEntity = new List<ControlEntity>();
            List<ComboBoxPair> cbp = new List<ComboBoxPair>();
            List<ComboBoxPair> cbp1 = new List<ComboBoxPair>();
            List<ComboBoxPair> cbp2 = new List<ComboBoxPair>();
            List<ComboBoxPair> cbp3 = new List<ComboBoxPair>();
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.Heading;
            myControlEntity.ID = "lbl";
            myControlEntity.Text = "Find In Files";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ColumnNumber = 0;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

            intRowCtr++;
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.Label;
            myControlEntity.ID = "lblFindWhat";
            myControlEntity.Text = "FindWhat";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.Width = 150;
            myControlEntity.ColumnNumber = 0;
            myControlEntity.ColumnSpan = 1;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

  

            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.ComboBox;
            fileName = "cbxFindWhatSelectedValue.txt";
            myControlEntity.SelectedValue = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
            myControlEntity.ID = "cbxFindWhat";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ToolTipx = "";
            //foreach (var item in alcbxFindWhat) {
            //    cbp.Add(new ComboBoxPair(item.ToString(), item.ToString()));
            //}
            //myControlEntity.ListOfKeyValuePairs = cbp;
            myControlEntity.ComboBoxIsEditable = true;
            myControlEntity.ColumnNumber = 1;

            myControlEntity.ColumnSpan = 2;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());


            intRowCtr++;
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.Label;
            myControlEntity.ID = "lblFileType";
            myControlEntity.Text = "FileType";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.Width = 150;
            myControlEntity.ColumnNumber = 0;
            myControlEntity.ColumnSpan = 1;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());


            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.ComboBox;
            fileName = "cbxFileTypeSelectedValue.txt";
            myControlEntity.SelectedValue = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
            myControlEntity.ID = "cbxFileType";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ToolTipx = "";
            //foreach (var item in alcbxFileType) {
            //    cbp1.Add(new ComboBoxPair(item.ToString(), item.ToString()));
            //}
            //myControlEntity.ListOfKeyValuePairs = cbp1;
            myControlEntity.ComboBoxIsEditable = true;
            myControlEntity.ColumnNumber = 1;
            myControlEntity.ColumnSpan = 2;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());


            intRowCtr++;
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.Label;
            myControlEntity.ID = "lblExclude";
            myControlEntity.Text = "Exclude";
            myControlEntity.Width = 150;
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ColumnNumber = 0;
            myControlEntity.ColumnSpan = 1;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());


            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.ComboBox;
            fileName = "cbxExcludeSelectedValue.txt";
            myControlEntity.SelectedValue = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
            myControlEntity.ID = "cbxExclude";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ToolTipx = "";
            myControlEntity.ComboBoxIsEditable = true;
            myControlEntity.ColumnNumber = 1;
            myControlEntity.ColumnSpan = 2;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());


            intRowCtr++;
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.Label;
            myControlEntity.ID = "lblFolder";
            myControlEntity.Text = "Folder";
            myControlEntity.Width = 150;
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ColumnNumber = 0;
            myControlEntity.ColumnSpan = 1;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.ComboBox;
            fileName = "cbxFolderSelectedValue.txt";
            myControlEntity.SelectedValue = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
            myControlEntity.ID = "cbxFolder";
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ToolTipx = "";
            myControlEntity.ComboBoxIsEditable = true;
            myControlEntity.ColumnNumber = 1;
            myControlEntity.ColumnSpan = 2;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.Button;
            myControlEntity.ID = "btnSelectFolder";     
            myControlEntity.Text = "Select Folder...";            
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ColumnNumber = 3;
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

            intRowCtr++;
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.CheckBox;
            myControlEntity.ID = "chkMatchCase";
            myControlEntity.Text = "Match Case";
            myControlEntity.Width = 150;
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ColumnNumber = 0;
            myControlEntity.ColumnSpan = 1;
            fileName = "chkMatchCase.txt";
            string strMatchCase = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);

            if (strMatchCase.ToLower() == "true") {
                myControlEntity.Checked = true;
            } else {
                myControlEntity.Checked = false;
            }
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

            intRowCtr++;
            myControlEntity.ControlEntitySetDefaults();
            myControlEntity.ControlType = ControlType.CheckBox;
            myControlEntity.ID = "chkUseRegularExpression";
            myControlEntity.Text = "UseRegularExpression";
            myControlEntity.Width = 150;
            myControlEntity.RowNumber = intRowCtr;
            myControlEntity.ColumnNumber = 0;
            myControlEntity.ColumnSpan = 1;
            fileName = "chkUseRegularExpression.txt";
            string strUseRegularExpression = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
            if (strUseRegularExpression.ToLower() == "true") {
                myControlEntity.Checked = true;
            } else {
                myControlEntity.Checked = false;
            }
            myListControlEntity.Add(myControlEntity.CreateControlEntity());

            DisplayWindowAgain:
            string strButtonPressed = myActions.WindowMultipleControls(ref myListControlEntity, 300, 1200, 100, 100);
            LineAfterDisplayWindow:
            if (strButtonPressed == "btnCancel") {
                myActions.MessageBoxShow("Okay button not pressed - Script Cancelled");
                goto myExit;
            }

            boolMatchCase = myListControlEntity.Find(x => x.ID == "chkMatchCase").Checked;
            boolUseRegularExpression = myListControlEntity.Find(x => x.ID == "chkUseRegularExpression").Checked;

            string strFindWhat = myListControlEntity.Find(x => x.ID == "cbxFindWhat").SelectedValue;
          //  string strFindWhatKey = myListControlEntity.Find(x => x.ID == "cbxFindWhat").SelectedKey;

            string strFileType = myListControlEntity.Find(x => x.ID == "cbxFileType").SelectedValue;
       //     string strFileTypeKey = myListControlEntity.Find(x => x.ID == "cbxFileType").SelectedKey;

            string strExclude = myListControlEntity.Find(x => x.ID == "cbxExclude").SelectedValue;
      //      string strExcludeKey = myListControlEntity.Find(x => x.ID == "cbxExclude").SelectedKey;

            string strFolder = myListControlEntity.Find(x => x.ID == "cbxFolder").SelectedValue;
            //     string strFolderKey = myListControlEntity.Find(x => x.ID == "cbxFolder").SelectedKey;
            fileName = "chkMatchCase.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, boolMatchCase.ToString());
            fileName = "chkUseRegularExpression.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, boolUseRegularExpression.ToString());
            fileName = "cbxFindWhatSelectedValue.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, strFindWhat);
            fileName = "cbxFileTypeSelectedValue.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, strFileType);
            fileName = "cbxExcludeSelectedValue.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, strExclude);
            fileName = "cbxFolderSelectedValue.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, strFolder);

            if (strButtonPressed == "btnSelectFolder") {
                fileName = "LastSearchFolder.txt";               
                 var dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.SelectedPath = myActions.ReadValueFromAppDataFile(settingsDirectory, fileName);
                string str = "LastSearchFolder";

               
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && Directory.Exists(dialog.SelectedPath)) {
                    myListControlEntity.Find(x => x.ID == "cbxFolder").SelectedValue = dialog.SelectedPath;
                    myListControlEntity.Find(x => x.ID == "cbxFolder").SelectedKey = dialog.SelectedPath;
                    myListControlEntity.Find(x => x.ID == "cbxFolder").Text = dialog.SelectedPath;                   
                    fileName = "LastSearchFolder.txt";
                    myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, dialog.SelectedPath);                   
                    strFolder = dialog.SelectedPath;
                    fileName = "cbxFolderSelectedValue.txt";
                    myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, strFolder);
                    strScriptName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    fileName = "cbxFolder.txt";
                    string settingsPath = System.IO.Path.Combine(settingsDirectory, fileName);
                    ArrayList alHosts = new ArrayList();
                    cbp = new List<ComboBoxPair>();
                    cbp.Clear();
                    cbp.Add(new ComboBoxPair("--Select Item ---", "--Select Item ---"));
                    ComboBox myComboBox = new ComboBox();
 

                        if (!File.Exists(settingsPath)) {
                            using (StreamWriter objSWFile = File.CreateText(settingsPath)) {
                                objSWFile.Close();
                            }
                        }
                        using (StreamReader objSRFile = File.OpenText(settingsPath)) {
                            string strReadLine = "";
                            while ((strReadLine = objSRFile.ReadLine()) != null) {
                                string[] keyvalue = strReadLine.Split('^');
                                if (keyvalue[0] != "--Select Item ---") {
                                    cbp.Add(new ComboBoxPair(keyvalue[0], keyvalue[1]));
                                }
                            }
                            objSRFile.Close();
                        }
                       string strNewHostName = dialog.SelectedPath;
                    List<ComboBoxPair> alHostx = cbp;
                    List<ComboBoxPair> alHostsNew = new List<ComboBoxPair>();
                    ComboBoxPair myCbp = new ComboBoxPair(strNewHostName, strNewHostName);
                    bool boolNewItem = false;

                    alHostsNew.Add(myCbp);
                    if (alHostx.Count > 14) {
                        for (int i = alHostx.Count - 1; i > 0; i--) {
                            if (alHostx[i]._Key.Trim() != "--Select Item ---") {
                                alHostx.RemoveAt(i);
                                break;                            
                            }
                        }
                    }
                    foreach (ComboBoxPair item in alHostx) {                        
                        if (strNewHostName != item._Key && item._Key != "--Select Item ---") {
                            boolNewItem = true;
                            alHostsNew.Add(item);
                        }                        
                    }

                    using (StreamWriter objSWFile = File.CreateText(settingsPath)) {
                        foreach (ComboBoxPair item in alHostsNew) {
                            if (item._Key != "") {
                                objSWFile.WriteLine(item._Key + '^' + item._Value);
                            }
                        }
                        objSWFile.Close();
                    }
                    goto DisplayWindowAgain;
                }
            }
            string strFindWhatToUse = "";
            string strFileTypeToUse = "";
            string strExcludeToUse = "";
            string strFolderToUse = "";
            if (strButtonPressed == "btnOkay") {
                if ((strFindWhat == "--Select Item ---" || strFindWhat == "")) {
                    myActions.MessageBoxShow("Please enter Find What or select Find What from ComboBox; else press Cancel to Exit");
                    goto DisplayFindTextInFilesWindow;
                }
                if ((strFileType == "--Select Item ---" || strFileType == "")) {
                    myActions.MessageBoxShow("Please enter File Type or select File Type from ComboBox; else press Cancel to Exit");
                    goto DisplayFindTextInFilesWindow;
                }
                if ( (strExclude == "--Select Item ---" || strExclude == "")) {
                    myActions.MessageBoxShow("Please enter Exclude or select Exclude from ComboBox; else press Cancel to Exit");
                    goto DisplayFindTextInFilesWindow;
                }
                if ( (strFolder == "--Select Item ---" || strFolder == "")) {
                    myActions.MessageBoxShow("Please enter Folder or select Folder from ComboBox; else press Cancel to Exit");
                    goto DisplayFindTextInFilesWindow;
                }


  
                    strFindWhatToUse = strFindWhat;
 
                if (boolUseRegularExpression) {
                    strFindWhatToUse = strFindWhatToUse.Replace(")", "\\)").Replace("(", "\\(");
                }

 
                    strFileTypeToUse = strFileType;



                    strExcludeToUse = strExclude;

 
                    strFolderToUse = strFolder;


            }


            strPathToSearch = strFolderToUse;

            strSearchPattern = strFileTypeToUse;

            strSearchExcludePattern = strExcludeToUse;

            strSearchText = strFindWhatToUse;

            strLowerCaseSearchText = strFindWhatToUse.ToLower();
            fileName = "FindWhatToUse.txt";
            myActions.WriteValueToAppDirectoryFile(settingsDirectory, fileName, strFindWhatToUse);
 
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            intHits = 0;
            int intLineCtr;
            List<FileInfo> myFileList = TraverseTree(strSearchPattern, strPathToSearch);
            int intFiles = 0;
            matchInfoList = new List<MatchInfo>();
            //         myFileList = myFileList.OrderBy(fi => fi.FullName).ToList();
            Parallel.ForEach(myFileList, myFileInfo => {
                intLineCtr = 0;
                boolStringFoundInFile = false;
                ReadFileToString(myFileInfo.FullName, intLineCtr, matchInfoList);
                if (boolStringFoundInFile) {
                    intFiles++;
                }
            });
            matchInfoList = matchInfoList.Where(mi => mi != null).OrderBy(mi => mi.FullName).ThenBy(mi => mi.LineNumber).ToList();
            List<string> lines = new List<string>();
            foreach (var item in matchInfoList) {
                lines.Add("\"" + item.FullName + "\"(" + item.LineNumber + "," + item.LinePosition + "): " + item.LineText.Length.ToString() + " " + item.LineText.Substring(0, item.LineText.Length > 5000 ? 5000 : item.LineText.Length));

            }
            //   System.IO.File.WriteAllLines(@"C:\Data\MatchInfo1.txt", lines);
            using (FileStream fs = new FileStream(@"C:\Data\MatchInfo.txt", FileMode.Create)) {
                StreamWriter file = new System.IO.StreamWriter(fs, Encoding.Default);

                file.WriteLine(@"-- " + strSearchText + " in " + strPathToSearch + " from " + strSearchPattern + " excl  " + strSearchExcludePattern + " --");
                foreach (var item in matchInfoList) {
                    file.WriteLine("\"" + item.FullName + "\"(" + item.LineNumber + "," + item.LinePosition + "): " + item.LineText.Substring(0, item.LineText.Length > 5000 ? 5000 : item.LineText.Length));
                }
                int intUniqueFiles = matchInfoList.Select(x => x.FullName).Distinct().Count();
                st.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = st.Elapsed;
                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                file.WriteLine("RunTime " + elapsedTime);
                file.WriteLine(intHits.ToString() + " hits");
                // file.WriteLine(myFileList.Count().ToString() + " files");           
                file.WriteLine(intUniqueFiles.ToString() + " files with hits");
                file.Close();


                // Get the elapsed time as a TimeSpan value.
                ts = st.Elapsed;
                // Format and display the TimeSpan value.
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                   ts.Hours, ts.Minutes, ts.Seconds,
                   ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);
                Console.WriteLine(intHits.ToString() + " hits");
                // Console.WriteLine(myFileList.Count().ToString() + " files");
                Console.WriteLine(intUniqueFiles.ToString() + " files with hits");
                Console.ReadLine();
                string strExecutable = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
                string strContent = @"C:\Data\MatchInfo.txt";
                Process.Start(strExecutable, string.Concat("", strContent, ""));

                myActions.MessageBoxShow("RunTime: " + elapsedTime + "\n\r\n\rHits: " + intHits.ToString() + "\n\r\n\rFiles with hits: " + intUniqueFiles.ToString());
            }


            if (strButtonPressed == "btnOkay") {
                strButtonPressed = myActions.WindowMultipleControlsMinimized(ref myListControlEntity, 300, 1200, 100, 100);
                goto LineAfterDisplayWindow;
            }


            myExit:
            Application.Current.Shutdown();
        }
        public static List<FileInfo> TraverseTree(string filterPattern, string root) {
            string[] arrayExclusionPatterns = strSearchExcludePattern.Split(';');
            for (int i = 0; i < arrayExclusionPatterns.Length; i++) {
                arrayExclusionPatterns[i] = arrayExclusionPatterns[i].ToLower().ToString().Replace("*", "");
            }
            List<FileInfo> myFileList = new List<FileInfo>();
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);
         
                if (!System.IO.Directory.Exists(root)) {
                    MessageBox.Show(root + " - folder did not exist");
                }


            dirs.Push(root);

            while (dirs.Count > 0) {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The 
                // choice of which exceptions to catch depends entirely on the specific task 
                // you are intending to perform and also on how much you know with certainty 
                // about the systems on which this code will run.
                catch (UnauthorizedAccessException e) {
                    Console.WriteLine(e.Message);
                    continue;
                } catch (System.IO.DirectoryNotFoundException e) {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try {
                    files = System.IO.Directory.GetFiles(currentDir, filterPattern);
                } catch (UnauthorizedAccessException e) {

                    Console.WriteLine(e.Message);
                    continue;
                } catch (System.IO.DirectoryNotFoundException e) {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files) {
                    try {
                        // Perform whatever action is required in your scenario.
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        bool boolFileHasGoodExtension = true;
                        foreach (var item in arrayExclusionPatterns) {
                            if (fi.FullName.ToLower().Contains(item)) {
                                boolFileHasGoodExtension = false;
                            }
                        }
                        if (boolFileHasGoodExtension) {
                            myFileList.Add(fi);
                        }
                        //    Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                    } catch (System.IO.FileNotFoundException e) {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
            return myFileList;
        }
        public static void ReadFileToString(string fullFilePath, int intLineCtr, List<MatchInfo> matchInfoList) {
            while (true) {
                try {
                    using (FileStream fs = new FileStream(fullFilePath, FileMode.Open)) {
                        using (StreamReader sr = new StreamReader(fs, Encoding.Default)) {
                            string s;
                            string s_lower = "";
                            while ((s = sr.ReadLine()) != null) {
                                intLineCtr++;
                                if (boolUseRegularExpression) {
                                    if (boolMatchCase) {
                                        if (System.Text.RegularExpressions.Regex.IsMatch(s, strSearchText, System.Text.RegularExpressions.RegexOptions.None)) {
                                            intHits++;
                                            boolStringFoundInFile = true;
                                            MatchInfo myMatchInfo = new MatchInfo();
                                            myMatchInfo.FullName = fullFilePath;
                                            myMatchInfo.LineNumber = intLineCtr;
                                            myMatchInfo.LinePosition = s.IndexOf(strSearchText) + 1;
                                            myMatchInfo.LineText = s;
                                            matchInfoList.Add(myMatchInfo);
                                        }
                                    } else {
                                        s_lower = s.ToLower();
                                        if (System.Text.RegularExpressions.Regex.IsMatch(s_lower, strLowerCaseSearchText, System.Text.RegularExpressions.RegexOptions.IgnoreCase)) {
                                            intHits++;
                                            boolStringFoundInFile = true;
                                            MatchInfo myMatchInfo = new MatchInfo();
                                            myMatchInfo.FullName = fullFilePath;
                                            myMatchInfo.LineNumber = intLineCtr;
                                            myMatchInfo.LinePosition = s_lower.IndexOf(strLowerCaseSearchText) + 1;
                                            myMatchInfo.LineText = s;
                                            matchInfoList.Add(myMatchInfo);
                                        }
                                    }
                                } else {
                                    if (boolMatchCase) {
                                        if (s.Contains(strSearchText)) {
                                            intHits++;
                                            boolStringFoundInFile = true;
                                            MatchInfo myMatchInfo = new MatchInfo();
                                            myMatchInfo.FullName = fullFilePath;
                                            myMatchInfo.LineNumber = intLineCtr;
                                            myMatchInfo.LinePosition = s.IndexOf(strSearchText) + 1;
                                            myMatchInfo.LineText = s;
                                            matchInfoList.Add(myMatchInfo);
                                        }
                                    } else {
                                        s_lower = s.ToLower();
                                        if (s_lower.Contains(strLowerCaseSearchText)) {

                                            intHits++;
                                            boolStringFoundInFile = true;
                                            MatchInfo myMatchInfo = new MatchInfo();
                                            myMatchInfo.FullName = fullFilePath;
                                            myMatchInfo.LineNumber = intLineCtr;
                                            myMatchInfo.LinePosition = s_lower.IndexOf(strLowerCaseSearchText) + 1;
                                            myMatchInfo.LineText = s;
                                            matchInfoList.Add(myMatchInfo);
                                        }
                                    }
                                }
                            }
                            return;
                        }

                    }
                } catch (FileNotFoundException ex) {
                    Console.WriteLine("Output file {0} not yet ready ({1})", fullFilePath, ex.Message);
                    break;
                } catch (IOException ex) {
                    Console.WriteLine("Output file {0} not yet ready ({1})", fullFilePath, ex.Message);
                    break;
                } catch (UnauthorizedAccessException ex) {
                    Console.WriteLine("Output file {0} not yet ready ({1})", fullFilePath, ex.Message);
                    break;
                }
            }
        }
        private string GetAppDirectoryForScript(string strScriptName) {
            string settingsDirectory =
      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\IdealAutomate\\" + strScriptName;
            if (!Directory.Exists(settingsDirectory)) {
                Directory.CreateDirectory(settingsDirectory);
            }
            return settingsDirectory;
        }
        private ArrayList ReadAppDirectoryFileToArrayList(string settingsDirectory, string fileName) {
            ArrayList myArrayList = new ArrayList();
            string settingsPath = Path.Combine(settingsDirectory, fileName);
            StreamReader reader = File.OpenText(settingsPath);
            while (!reader.EndOfStream) {
                string myLine = reader.ReadLine();
                myArrayList.Add(myLine);
            }
            reader.Close();
            return myArrayList;
        }
        private void WriteArrayListToAppDirectoryFile(string settingsDirectory, string fileName, ArrayList arrayListToWrite) {
            StreamWriter writer = null;
            string settingsPath = Path.Combine(settingsDirectory, fileName);
            // Hook a write to the text file.
            writer = new StreamWriter(settingsPath);
            // Rewrite the entire value of s to the file
            foreach (var item in arrayListToWrite) {
                writer.WriteLine(item.ToString());
            }
            writer.Close();
        }
        private bool DoesSettingExist(string settingName) {
            return FindTextInFiles.Properties.Settings.Default.Properties.Cast<SettingsProperty>().Any(prop => prop.Name == settingName);
        }
    }
}