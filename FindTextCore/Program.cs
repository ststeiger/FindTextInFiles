
using System;
using System.Linq;


namespace FindTextCore
{
    
    
    class Program
    {
        private static int intHits;

        private static bool boolUseRegularExpression = false;
        private static bool boolMatchCase = false;
        private static bool boolStringFoundInFile = false;
        private static string strSearchText;
        private static string strLowerCaseSearchText;
        private static string strSearchExcludePattern;
        
        
        


        public static void ReadFileToString(string fullFilePath, int intLineCtr
            , System.Collections.Generic.List<MatchInfo> matchInfoList)
        {
            while (true)
            {
                try
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(fullFilePath, System.IO.FileMode.Open))
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, System.Text.Encoding.Default))
                        {
                            string s;
                            string s_lower = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                intLineCtr++;
                                if (boolUseRegularExpression)
                                {
                                    if (boolMatchCase)
                                    {
                                        if (System.Text.RegularExpressions.Regex.IsMatch(s, strSearchText, System.Text.RegularExpressions.RegexOptions.None))
                                        {
                                            intHits++;
                                            boolStringFoundInFile = true;
                                            MatchInfo myMatchInfo = new MatchInfo();
                                            myMatchInfo.FullName = fullFilePath;
                                            myMatchInfo.LineNumber = intLineCtr;
                                            myMatchInfo.LinePosition = s.IndexOf(strSearchText) + 1;
                                            myMatchInfo.LineText = s;
                                            matchInfoList.Add(myMatchInfo);
                                        }
                                    }
                                    else
                                    {
                                        s_lower = s.ToLower();
                                        if (System.Text.RegularExpressions.Regex.IsMatch(s_lower, strLowerCaseSearchText, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                        {
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
                                else
                                {
                                    if (boolMatchCase)
                                    {
                                        if (s.Contains(strSearchText))
                                        {
                                            intHits++;
                                            boolStringFoundInFile = true;
                                            MatchInfo myMatchInfo = new MatchInfo();
                                            myMatchInfo.FullName = fullFilePath;
                                            myMatchInfo.LineNumber = intLineCtr;
                                            myMatchInfo.LinePosition = s.IndexOf(strSearchText) + 1;
                                            myMatchInfo.LineText = s;
                                            matchInfoList.Add(myMatchInfo);
                                        }
                                    }
                                    else
                                    {
                                        s_lower = s.ToLower();
                                        if (s_lower.Contains(strLowerCaseSearchText))
                                        {

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
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    System.Console.WriteLine("Output file {0} not yet ready ({1})", fullFilePath, ex.Message);
                    break;
                }
                catch (System.IO.IOException ex)
                {
                    System.Console.WriteLine("Output file {0} not yet ready ({1})", fullFilePath, ex.Message);
                    break;
                }
                catch (System.UnauthorizedAccessException ex)
                {
                    System.Console.WriteLine("Output file {0} not yet ready ({1})", fullFilePath, ex.Message);
                    break;
                }
            }
        }

        public static System.Collections.Generic.List<System.IO.FileInfo> 
            TraverseTree(string filterPattern, string root)
        {
            string[] arrayExclusionPatterns = strSearchExcludePattern.Split(';');
            for (int i = 0; i < arrayExclusionPatterns.Length; i++)
            {
                arrayExclusionPatterns[i] = arrayExclusionPatterns[i].ToLower().ToString().Replace("*", "");
            }
            
            System.Collections.Generic.List<System.IO.FileInfo> myFileList = 
                new System.Collections.Generic.List<System.IO.FileInfo>();
            
            // Data structure to hold names of subfolders to be
            // examined for files.
            System.Collections.Generic.Stack<string> dirs = 
                new System.Collections.Generic.Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                System.Console.WriteLine(root + " - folder did not exist");
                // MessageBox.Show(root + " - folder did not exist");
            }


            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
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
                catch (System.UnauthorizedAccessException e)
                {
                    System.Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    System.Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir, filterPattern);
                }
                catch (System.UnauthorizedAccessException e)
                {

                    System.Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    System.Console.WriteLine(e.Message);
                    continue;
                }

                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files)
                {
                    try
                    {
                        // Perform whatever action is required in your scenario.
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        bool boolFileHasGoodExtension = true;
                        foreach (var item in arrayExclusionPatterns)
                        {
                            if (fi.FullName.ToLower().Contains(item))
                            {
                                boolFileHasGoodExtension = false;
                            }
                        }
                        if (boolFileHasGoodExtension)
                        {
                            myFileList.Add(fi);
                        }
                        //    Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        System.Console.WriteLine(e.Message);
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

        static void Main(string[] args)
        {

            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            /*
        private static bool boolUseRegularExpression;
        private static bool boolMatchCase;
        private static bool boolStringFoundInFile;
        private static string strSearchText;
        private static string strLowerCaseSearchText;
        private static string strSearchExcludePattern;
*/



            strSearchText = "notepad";
            strLowerCaseSearchText = strSearchText.ToLower();
            strSearchExcludePattern = "*.js";

            
            string strSearchPattern = "*.cs";
            string strPathToSearch = "/root/github/ststeiger/FindTextInFiles/FindTextInFiles/";
            
            intHits = 0;
            System.Collections.Generic.List<System.IO.FileInfo> myFileList = TraverseTree(strSearchPattern, strPathToSearch);
            int intFiles = 0;
            int intLineCtr;
            System.Collections.Generic.List<MatchInfo> matchInfoList = 
                new System.Collections.Generic.List<MatchInfo>();
            
            // myFileList = myFileList.OrderBy(fi => fi.FullName).ToList();
            System.Threading.Tasks.Parallel.ForEach(myFileList, myFileInfo => {
                intLineCtr = 0;
                boolStringFoundInFile = false;
                ReadFileToString(myFileInfo.FullName, intLineCtr, matchInfoList);
                if (boolStringFoundInFile)
                {
                    intFiles++;
                }
            });
            
            matchInfoList = matchInfoList.Where(mi => mi != null).OrderBy(mi => mi.FullName).ThenBy(mi => mi.LineNumber).ToList();
            System.Collections.Generic.List<string> lines = 
                new System.Collections.Generic.List<string>();
            
            foreach (var item in matchInfoList)
            {
                lines.Add("\"" + item.FullName 
                          + "\"(" + item.LineNumber + "," + item.LinePosition + "): " 
                          + item.LineText.Length.ToString() 
                          + " " 
                          + item.LineText.Substring(0, item.LineText.Length > 5000 ? 5000 : item.LineText.Length));
            }

            string searchResultsFile = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);
            searchResultsFile = System.IO.Path.Combine(searchResultsFile, "MatchInfo.txt");
            
            
            using (System.IO.FileStream fs = new System.IO.FileStream(searchResultsFile, System.IO.FileMode.Create))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);

                file.WriteLine(@"-- " + strSearchText + " in " + strPathToSearch + " from " + strSearchPattern + " excl  " + strSearchExcludePattern + " --");
                foreach (var item in matchInfoList)
                {
                    file.WriteLine("\"" + item.FullName + "\"(" + item.LineNumber + "," + item.LinePosition + "): " + item.LineText.Substring(0, item.LineText.Length > 5000 ? 5000 : item.LineText.Length));
                }
                int intUniqueFiles = matchInfoList.Select(x => x.FullName).Distinct().Count();
                st.Stop();
                // Get the elapsed time as a TimeSpan value.
                System.TimeSpan ts = st.Elapsed;
                // Format and display the TimeSpan value.
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
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
                elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                   ts.Hours, ts.Minutes, ts.Seconds,
                   ts.Milliseconds / 10);
                System.Console.WriteLine("RunTime " + elapsedTime);
                System.Console.WriteLine(intHits.ToString() + " hits");
                // Console.WriteLine(myFileList.Count().ToString() + " files");
                System.Console.WriteLine(intUniqueFiles.ToString() + " files with hits");
                
                
                string strExecutable = @"notepad.exe";
                if (System.Environment.OSVersion.Platform == PlatformID.Unix)
                    strExecutable = "gedit";
                
                System.Diagnostics.Process.Start(strExecutable, string.Concat("", searchResultsFile, ""));
                // myActions.ScriptEndedSuccessfullyUpdateStats();
                // myActions.MessageBoxShow("RunTime: " + elapsedTime + "\n\r\n\rHits: " + intHits.ToString() + "\n\r\n\rFiles with hits: " + intUniqueFiles.ToString() + "\n\r\n\rPut Cursor on line and\n\r press Ctrl+Alt+N\n\rto view detail page. ");
            }

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        }
    }
}
