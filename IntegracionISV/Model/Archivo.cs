namespace IntegracionISV.Model
{
    class Archivo
    {
        public string pathExtract { get; set; }
        public string pathZip { get; set; }
        public string [] allFiles { get; set; }
        public string [] allFilesZip { get; set; }
        public string [] allFilesCsv { get; set; }
        public string csvFile { get; set; }
        public string csvRead { get; set;  }
        public string zipFile { get; set; }
        public string link { get; set; }

        public Archivo()
        {
            string pathExtract = @"C:\ISV_Files_ReproSema\Extract\";
            string pathZip = @"C:\ISV_Files_ReproSema\";
            this.pathExtract = pathExtract;
            this.pathZip = pathZip;
        }
    }
}
