using AnrixApp.Models;
using AnrixApp.Services;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AnrixApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupsPage : ContentPage
	{
        public delegate void FileUploaded(Faculty faculty);
        public static event FileUploaded OnUpload;

        public GroupsPage ()
		{
            BindingContext = MockFacultyData.getMockicngFaculty().getGroups();
            InitializeComponent();
            OnUpload += delegate (Faculty faculty)
            {
                BindingContext = null;
                BindingContext = faculty.getGroups();
            };

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData filedata = await CrossFilePicker.Current.PickFile();
                var faculty = FileReaderService.ReadFromFile(filedata.FilePath);
                OnUpload(faculty);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void setBindingContext(Faculty faculty)
        {
            Group megaGroup = new Group("000", 0);
            BindingContext = null;
            BindingContext = faculty.getGroups();
            
            foreach (var temp in faculty.getGroups())
            {
                foreach (var tempS in temp)
                    megaGroup.Add(tempS);
            }

            //this.LoadFromXaml(typeof(StudentsPage)).BindingContext = null;
            this.LoadFromXaml(typeof(StudentsPage)).BindingContext = StudentsPage.allStudents = megaGroup;
            Debug.WriteLine((this.LoadFromXaml(typeof(StudentsPage)).BindingContext as Group).Students.Count);
        }
    }
}