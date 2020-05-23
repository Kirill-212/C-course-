using course.Models;
using course.ViewModels.costs;
using course.ViewModels.EntranceOrRegister;
using course.ViewModels.main;
using course.ViewModels.Tasks;
using course.ViewModels.timetable;
using course.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Security.Cryptography;
using System.IO;
using course.Repository;

namespace course.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        public static ModelDATABASE modelDATABASE = ModelDATABASE.getInstance();
        public EntranceCommand entrance { get; private set; }
        public CheckforValidRegCommand checkfor { get; private set; }

        public OpenWindowsCommand OpenWindowsCommand1 { get; private set; }


        public ViewCommand viewcommand { get; private set; }
        public AddTaskCommand addTask { get; private set; }
        public DelCommand delCommand { get; private set; }
        public ChangeCommand ChangeCommand { get; private set; } 
        public SaveCommand saveCommand { get; private set; }
        public OpenTaskWin OpenTask { get;private set; }




        public OpenTimeTableCommand openTimeTable { get; private set; }
        public AddSubCommand AddSubCommand { get; private set; }
        public ViewTimeTableCommand viewTimeTable { get; private set; }
        public SaveChCommand saveCh { get; private set; }
        public ClearTimeTableCommand ClearTimeTableCommand { get; private set; }



        public OpenCostsCommand openCosts { get; private set; }
        public AddCostsCommand AddCosts { get; private set; }
        public ViewCostsCommand viewCosts { get; private set; }
        public AddCostsCommand AddCostsCommand { get; private set; }
        public DelAllCostsCommand DelAllCostsCommand { get; private set; }
        public ChangeCostsCommand ChangeCostsCommand { get; private set; }



        public ViewMainCommand viewMain { get; private set; }
        public ChangeTaskCommand ChangeTask { get; private set; }
        public SendMesTaskCommand SendMesTask { get; private set; }
        public SendMesTimeTableCommand SendMesTimeTable { get; private set; }
        public SendMesCostsCommand SendMesCosts { get; private set; }
        public AdminCommand AdminCommand { get; private set; }
        public OpenAdminCommand OpenAdmin { get; private set; }
        public ViewAdminCommand ViewAdmin { get; private set; }

        private UnitOfWork unitofwork = new UnitOfWork();
        public ApplicationViewModel()
        {
            OpenWindowsCommand1 = new OpenWindowsCommand(output);
            entrance = new EntranceCommand(Entrance);
            checkfor = new CheckforValidRegCommand(checkforvalidReg);
            addTask = new AddTaskCommand(AddTask);
            viewcommand = new ViewCommand(Viewcommand);
            delCommand = new DelCommand(Del);
            ChangeCommand = new ChangeCommand(StatusTasks);
            saveCommand = new SaveCommand(Save);
            OpenTask = new OpenTaskWin(OpenTask1);


            openTimeTable = new OpenTimeTableCommand(OpenTimeTableWin);
            AddSubCommand = new AddSubCommand(AddSub);
            viewTimeTable = new ViewTimeTableCommand(viewtable);
            saveCh = new SaveChCommand(SaveCh);
            ClearTimeTableCommand = new ClearTimeTableCommand(clearTT);


            openCosts = new OpenCostsCommand(openCost);
            AddCosts = new AddCostsCommand(Addcosts);
            viewCosts = new ViewCostsCommand(viewC);
            AddCostsCommand = new AddCostsCommand(delC);
            DelAllCostsCommand = new DelAllCostsCommand(DelC_All);
            ChangeCostsCommand = new ChangeCostsCommand(ChC);


            viewMain = new ViewMainCommand(ViewMain);
            ChangeTask = new ChangeTaskCommand(ChangeTaks);
            SendMesTask = new SendMesTaskCommand(SendMesTASK);
            SendMesTimeTable = new SendMesTimeTableCommand(SendMesTB);
            SendMesCosts = new SendMesCostsCommand(SendMescosts);
            AdminCommand = new AdminCommand(Admin);
            OpenAdmin = new OpenAdminCommand(openAdmin);
            ViewAdmin = new ViewAdminCommand(viewAdmin);
        }


        Guid GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash);
        }

        #region Для входа 
        public void output()
        {
            Recording recording = new Recording();
            recording.ShowDialog();
        }
        public void Entrance()//вход
        {
            try
            {
                int m = 1;
                //ApplicationViewModel.modelDATABASE.Users.;
                //var result = ApplicationViewModel.modelDATABASE.Users.Where(p => p.Логин == login).Where(p => p.Пароль == password);
                //if (result != null)
                //{
                //    m=0;
                //}
                if (password != null && password != null)
                {
                    //  MessageBox.Show((password + '.').ToString());
                    foreach (User i in ApplicationViewModel.modelDATABASE.Users)
                    {
                        if (login == i.Логин)
                        {
                            if (GetHashString(password + '.') == i.Пароль)
                            {
                                m = 0;
                            }
                        }
                    }
                    if (m == 0)
                    {

                        Authorization = "Successfull";
                        MainWindowForProject mainWindowForProject = new MainWindowForProject();
                        ApplicationViewModel.Login_aut_user = login;
                        Login = "";
                        Password = default;
                        mainWindowForProject.ShowDialog();

                    }
                    else
                    {
                        Authorization = "Error:Указанный пользователь не зарегистрирован";
                    }
                }
                else
                {
                    Authorization = "Error:Заполните все поля";
                }
            }
            catch(Exception e)
            {
                Authorization = "Error:"+e.Message;
            }
           
        }
        public static string Login_aut_user;
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }
        private string password;
        public string Password
        {
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string authorization;
         public string Authorization
        {
            set { 
                authorization=value;
                RaisePropertyChanged("Authorization");
            }
            get {
                return authorization;
            }
        }
        #endregion

        #region Для регистрации
        public void checkforvalidReg() //для регистрации
        {
            string regex_for_email = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string regex_for_login = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$";
            try
            {
                if (e_mail_for_reg != null && login_for_reg != null && password_for_reg != null)
                {
                    int m = 0;
                    if (Regex.IsMatch(e_mail_for_reg, regex_for_email, RegexOptions.IgnoreCase))
                    {
                        foreach (User i in ApplicationViewModel.modelDATABASE.Users)
                        {
                            if (e_mail_for_reg == i.e_maeil)
                            {
                                m = 1;
                                Status = "Error: Пользователь с таким email уже существует";
                                break;
                            }

                        }

                    }
                    else
                    {
                        m = 1;
                        Status = "Error:Email не подтвержден";
                    }
                    if (Regex.IsMatch(login_for_reg, regex_for_login, RegexOptions.IgnoreCase))
                    {
                        foreach (User i in ApplicationViewModel.modelDATABASE.Users)
                        {
                            if (login_for_reg == i.Логин)
                            {
                                m = 1;
                                Status = "Error: Пользователь с таким логином уже существует";
                            }
                        }
                    }
                    else
                    {
                        Status = "Error:Неверно указан логин";
                        m = 1;
                    }
                    if (m != 1)
                    {
                        User user = new User();
                        user.e_maeil = e_mail_for_reg;
                        user.Логин = login_for_reg;
                        user.Пароль = GetHashString(password_for_reg + '.');
                        Status = "Successfull";
                        E_mail_for_reg = "";
                        Login_for_reg = "";
                        Password_for_reg = default;
                        ApplicationViewModel.modelDATABASE.Users.Add(user);
                        ApplicationViewModel.modelDATABASE.SaveChanges();
                        System.Windows.MessageBox.Show("e_mail  " + user.e_maeil + "|Login  " + user.Логин + "|Password  " + user.Пароль.ToString());


                    }
                }
                else{
                    Status = "Error:Заполните все поля";
                }
            }
            catch(Exception e)
            {
                Status = e.Message;
            }


        }
        private string e_mail_for_reg;
        public string E_mail_for_reg
        {
            set
            {
                e_mail_for_reg = value;
                RaisePropertyChanged("E_mail_for_reg");
            }
        }
        private string login_for_reg;
        public string Login_for_reg
        {
            set
            {
                login_for_reg = value;
                RaisePropertyChanged("Login_for_reg");
            }
        }
        private string password_for_reg;
        public string Password_for_reg
        {
            set
            {
                password_for_reg = value;
                RaisePropertyChanged("Password_for_reg");
            }
        }

        private string status;
        public string Status
        {
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
            get
            {
                return status;
            }
        }
        #endregion

        #region Основное окно,различный приколюхи

        private string user_connect_login;
        public string User_connect_login
        {
            set
            {
                user_connect_login = value;
                System.Windows.MessageBox.Show(user_connect_login);
                RaisePropertyChanged("User_connect_login");
            }
        }
        #endregion

        #region Реализация Tasks окна

        public void OpenTask1()
        {
            Views.Tasks t = new Views.Tasks();
            t.ShowDialog();
        }


        ObservableCollection<task> TasksObserv = new ObservableCollection<task>();

        private string statusaddtask;
        public string StatusAddTask
        {
            set
            {
                statusaddtask = value;
                RaisePropertyChanged("StatusAddTask");
            }
            get
            {
                return statusaddtask;
            }
        }

        private string name;
        public string Name
        {
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string full_name;
        public string Full_name
        {
            set
            {
                full_name = value;
                RaisePropertyChanged("Full_name");
            }
        }

        private string periodicity;
        public string Periodicity
        {
            set
            {
                periodicity = value;
                RaisePropertyChanged("Periodicity");
            }
        }

        private int priorety;
        public int Priorety
        {
            set
            {
                priorety =Convert.ToInt32( value);
                RaisePropertyChanged("Priorety");
            }
        }
        private string date;
        public string Date
        {
            set
            {
                date=value;
                RaisePropertyChanged("Date");
            }
        }

        public void AddTask()
        {
            int m = 0;
            try
            {
                var result = ApplicationViewModel.modelDATABASE.tasks.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
                if (result.Count() <=10)
                {
                    //MessageBox.Show(name + "  " + name.Length.ToString());
                    //if (name.Length > 50) name = name.Substring(0, 49);
                    //MessageBox.Show(name + "  " + name.Length.ToString());
                    //MessageBox.Show(full_name + "  " + full_name.Length.ToString());
                    if (date.Length != 0)
                    {
                        int z = 0;
                        foreach (task i in ApplicationViewModel.modelDATABASE.tasks) z = i.id;
                        //MessageBox.Show(date);
                        foreach (Date i in ApplicationViewModel.modelDATABASE.Dates)
                        {
                            //   MessageBox.Show(i.Дата.ToString() + "    " + date);
                            if (i.Дата == date)
                            {
                                // MessageBox.Show(i.Дата.ToString() + " leave   " + date);
                                m = 1;
                                break;
                            }
                        }
                        if (m == 0)
                        {
                            Date date_tasks = new Date();

                            date_tasks.Дата = date;
                            //  MessageBox.Show(date_tasks.Дата.ToString() +"||||"+ date);
                            ApplicationViewModel.modelDATABASE.Dates.Add(date_tasks);
                            ApplicationViewModel.modelDATABASE.SaveChanges();
                            // MessageBox.Show(date_tasks.Дата.ToString() + "||||" + date);
                        }
                        if (name == null || name == "")
                        {
                            StatusAddTask = "Error:name";
                        }
                        else if (periodicity == null || periodicity == "")
                        {
                            StatusAddTask = "Error:periodicity";
                        }
                        else if (full_name == null || full_name == "")
                        {
                            StatusAddTask = "Error:full_name";
                        }
                        else if (priorety.ToString() == null || priorety.ToString() == "" || priorety == 0)
                        {
                            StatusAddTask = "Error:priorety";
                        }
                        else
                        {
                            try
                            {
                                task task_ = new task();
                                if (name.Length > 20) name = name.Substring(0, 20);
                                task_.Название = checked(name + '.');
                                task_.Периодичность = periodicity;
                                if (full_name.Length > 40) full_name = full_name.Substring(0, 40);
                                task_.Полное_описание = checked(full_name + '.');
                                task_.Дата = date;
                                task_.Приоритет = priorety;
                                task_.Status = 0;
                                task_.id = z + 1;
                                task_.Логин = ApplicationViewModel.Login_aut_user;
                                ApplicationViewModel.modelDATABASE.tasks.Add(task_);
                                ApplicationViewModel.modelDATABASE.SaveChanges();
                                StatusAddTask = "operation completed successfully";
                               // System.Windows.MessageBox.Show(priorety + " приоритет" + Environment.NewLine + periodicity + " периодичность|" + date + "|" + name + "|" + full_name + "|");
                                Viewcommand();
                                Name = null;
                                Full_name = null;
                            }
                            catch (System.OverflowException e)
                            {
                                StatusAddTask = "Error(ch): " + e.ToString();
                            }
                        }

                    }
                    else
                    {
                        StatusAddTask = "Error:date";
                    }
                }
                else
                {
                    StatusAddTask = "Error:Записей больше 10";
                }


            }
            catch(Exception e)
            {
                Name = null;
                Full_name = null;
                //System.Windows.MessageBox.Show(e.Message);
                StatusAddTask = e.Message;
            }
            
        }



        public void Viewcommand()
        {
            try {
            TasksObserv.Clear();
            foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
            {

                if (i.Логин == ApplicationViewModel.Login_aut_user)
                {
                    //System.Windows.MessageBox.Show(i.Логин);
                    TasksObserv.Add(i);
                }

            }
            }catch(Exception e)
            {
                StatusAddTask = e.Message;
            }

           // System.Windows.MessageBox.Show(TasksObserv.Count.ToString());
        }
        public ObservableCollection<task> _Tasks
        {
            get
            {
                return TasksObserv;
            }
            set { TasksObserv = value; }
        }


        private int selectindex;
        public int SelectIndex
        {
            get { return selectindex; }
            set
            {
                selectindex = value;
                //   MessageBox.Show(del_index.ToString());

            }
        }
        public void Sort()
        {
            int sortIndex = 0;
            if (sortIndex1 == "По приоритету") sortIndex = 0;
            else if (sortIndex1 == "По названию") sortIndex = 1;
            else if (sortIndex1 == "По Дате") sortIndex = 2;
            else if (sortIndex1 == "По Статусу") sortIndex = 3;
            else sortIndex = 4;

            ObservableCollection<task> TasksObserv1 = new ObservableCollection<task>();
            if (sortIndex == 0)
            {
                var sortedUsers = from u in TasksObserv
                                  orderby u.Приоритет
                                  select u;

                foreach (task u in sortedUsers)
                    TasksObserv1.Add(u);
                TasksObserv.Clear();
                foreach (task i in TasksObserv1)
                {

                    TasksObserv.Add(i);
                }
            }
            else if (sortIndex == 1)
            {
                var sortedUsers = from u in TasksObserv
                                  orderby u.Название
                                  select u;


                foreach (task u in sortedUsers)
                    TasksObserv1.Add(u);
                TasksObserv.Clear();
                foreach (task i in TasksObserv1)
                {

                    TasksObserv.Add(i);
                }
            }
            else if (sortIndex == 2)
            {
                var sortedUsers = from u in TasksObserv
                                  orderby u.Дата
                                  select u;

                foreach (task u in sortedUsers)
                    TasksObserv1.Add(u);
                TasksObserv.Clear();
                foreach (task i in TasksObserv1)
                {

                    TasksObserv.Add(i);
                }
            }
            else if (sortIndex == 3)
            {
                var sortedUsers = from u in TasksObserv
                                  orderby u.Status
                                  select u;
                

                foreach (task u in sortedUsers)
                    TasksObserv1.Add(u);
                TasksObserv.Clear();
                foreach (task i in TasksObserv1)
                {

                    TasksObserv.Add(i);
                }
            }


            
        }
        private string sortIndex1;
        public string SortIndex
        {
            set
            {
                sortIndex1 = value;
                RaisePropertyChanged("SortIndex");
                Sort();
            }
            get
            {
                return sortIndex1;
            }
        }
        private string delIndex1;
        public string DelIndex
        {
            set
            {
                delIndex1 = value;
                RaisePropertyChanged("DelIndex");
                //System.Windows.MessageBox.Show(delIndex1.ToString());
            }
            get
            {
                return delIndex1;
            }
        }

        public void Del()
        {
            if (TasksObserv.Count > 0)
            {
                int delIndex = 0;
                if (delIndex1 == "Удалить всё") delIndex = 0;
                else if (delIndex1 == "Удалить выбранный элемент") delIndex = 1;
                else if (delIndex1 == "удалить всё,кроме сегодня") delIndex = 2;
                else
                {
                    delIndex = 4;
                }
                DialogResult result;
                if (delIndex == 0)
                {
                    result = System.Windows.Forms.MessageBox.Show(
                   "Удалить всё",
                       "Удалить",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                    );

                }
                else if (delIndex == 1)
                {
                    result = System.Windows.Forms.MessageBox.Show(
                    "Удалить выбранный элемент",
                    "Удалить",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                    );

                }
                else if (delIndex == 2)
                {
                    result = System.Windows.Forms.MessageBox.Show(
                        "удалить всё, кроме сегодня",
                             "Удалить",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                    );

                }
                else
                {
                    result = DialogResult.No;
                    StatusAddTask = "Error:не выбрано как удалять";
                }

                if (result == DialogResult.Yes)
                {
                    if (delIndex == 0)
                    {
                       // System.Windows.MessageBox.Show("0");

                        foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
                        {
                            if (i.Логин == ApplicationViewModel.Login_aut_user)
                            {
                                ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Deleted;

                            }

                        }
                        ApplicationViewModel.modelDATABASE.SaveChanges();
                        StatusAddTask = "del completed successfully";
                    }
                    else if (delIndex == 1)
                    {
                        if (selectindex != -1)
                        {
                           // System.Windows.MessageBox.Show("1");
                            foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
                            {
                                if (i.Логин == ApplicationViewModel.Login_aut_user && TasksObserv[selectindex] == i)
                                {
                                    ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Deleted;

                                }

                            }
                            ApplicationViewModel.modelDATABASE.SaveChanges();
                            StatusAddTask = "del completed successfully";
                        }
                        else
                        {
                            StatusAddTask = "Error:удаление неозможно т.к. не выбран элемент";
                        }

                    }
                    else if (delIndex == 2)
                    {
                        foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
                        {
                            //System.Windows.MessageBox.Show(DateTime.Today.Year.ToString() + "." + DateTime.Today.Month.ToString() +
                            //    "." + DateTime.Today.ToString()
                            //     + "----" + i.Дата);
                            if (i.Логин == ApplicationViewModel.Login_aut_user && !(DateTime.Today.ToString() == i.Дата))
                            {
                                System.Windows.MessageBox.Show("удаляем" + i.Дата.ToString());
                                ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Deleted;

                            }

                        }
                        ApplicationViewModel.modelDATABASE.SaveChanges();
                        StatusAddTask = "del completed successfully";

                    }
                    //System.Windows.MessageBox.Show("нажал yes");
                    Viewcommand();
                }
            }
            else
            {
                StatusAddTask = "Error:нечего удалять";
            }
        }


        public void StatusTasks()
        {
            try
            {
                if (selectindex != -1 && TasksObserv.Count > 0)
                {
                    foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
                    {
                        if (i.Логин == ApplicationViewModel.Login_aut_user && TasksObserv[selectindex].id == i.id)
                        {


                            i.Status = 1;
                            ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Modified;
                        }

                    }
                    ApplicationViewModel.modelDATABASE.SaveChanges();
                    Viewcommand();
                    StatusAddTask = "task completed successfully";
                }
                else
                {
                    StatusAddTask = "Error:Не выбрали задание,которое надо выполнить\n|список задач пуст";
                }
            }catch(Exception e)
            {
                StatusAddTask = e.Message;
            }

        }

        private task selectedtasks;
        public task SelectedTasks
        {
            get { return selectedtasks; }
            set
            {
                selectedtasks = value;
                RaisePropertyChanged("SelectedTasks");
            }
        }

        private string name1;
        public string Name1
        {
            set
            {
                name1 = value;
                RaisePropertyChanged("Name1");
            }
        }
        private string full_name1;
        public string Full_name1
        {
            set
            {
                full_name1 = value;
                RaisePropertyChanged("Full_name1");
            }
        }
        public void Save()
        {
            try
            {
                if (full_name1 != null && name1 != null && selectindex != -1 && TasksObserv.Count > 0)
                {

                    if (name1.Length >20) name1 = name1.Substring(0, 20);
                    if (full_name1.Length > 40) full_name1 = full_name1.Substring(0, 40);
                    //System.Windows.MessageBox.Show("Периодичность-" + TasksObserv[selectindex].Периодичность + "|Приоритет-" + TasksObserv[selectindex].Приоритет + "|название-" +
                    //    TasksObserv[selectindex].Название);
                    TasksObserv[selectindex].Название = name1 + '.';
                    TasksObserv[selectindex].Полное_описание = full_name1 + '.';
                    ApplicationViewModel.modelDATABASE.Entry(TasksObserv[selectindex]).State = EntityState.Modified;
                    ApplicationViewModel.modelDATABASE.SaveChanges();
                    Viewcommand();
                    Full_name1 = "";
                    Name1 = "";
                    StatusAddTask = "change completed";
                }
                else
                {
                    StatusAddTask = "Error:не заполнены все поля|не выбрали элемент\n|список задач пуст";
                }
            }catch(Exception e)
            {
                StatusAddTask = "Error:" + e.Message;
            }
        }

        #endregion

        #region Реализация TimeTable

        private string statusTimeTable;
        public string StatusTimeTable
        {
            set
            {
                statusTimeTable = value;
                RaisePropertyChanged("StatusTimeTable");
            }
            get
            {
                return statusTimeTable;
            }
        }

        public void clearTT()
        {
            try
            {
                var result = ApplicationViewModel.modelDATABASE.Расписание.Where(p => p.Логин ==
                       ApplicationViewModel.Login_aut_user);
                foreach (Расписание i in result)
                {
                    ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Deleted;
                }
                ApplicationViewModel.modelDATABASE.SaveChanges();
                viewtable();
                StatusTimeTable = "clear completed";
            }catch(Exception e)
            {
                StatusTimeTable = e.Message;
            }
        }

        public void OpenTimeTableWin()
        {
            TimeTable time = new TimeTable();
            time.ShowDialog();

        }

      public static  ObservableCollection<TimeTableR> timetable = new ObservableCollection<TimeTableR>();

        private string day_of_the_week;
        public string Day_of_the_week
        {
            set
            {
                day_of_the_week = value;
                RaisePropertyChanged("Day_of_the_week");
            }
            get
            {
                return day_of_the_week;
            }
        }

        private int week;
        public string Week
        {
            set
            {
             //   MessageBox.Show(value);
                if (value == "Вторая") week = 2;
                else if (value == "Первая") week = 1;
                else week = 0;
                RaisePropertyChanged("Week");
            }

        }
        private string subject;
        public string Subject
        {
            set
            {
                subject = value;
                RaisePropertyChanged("Subject");
            }
            get
            {
                return subject;
            }
        }
        private string time;
        public string Time
        {
            set
            {
                time = value;
                RaisePropertyChanged("Time");
            }
            get
            {
                return time;
            }
        }
        private int selectindexTimetable;
        public int SelectIndexTB
        {
            get { return selectindexTimetable; }
            set
            {
                selectindexTimetable = value;
                //   MessageBox.Show(selectindexTimetable.ToString());

            }
        }
        public ObservableCollection<TimeTableR> _TimeTable
        {
            get
            {
                return timetable;
            }
            set { timetable = value; }
        }
        public void AddSub()
        {
            try
            {
                if (time.Length != 0 && day_of_the_week.Length != 0 && subject != null && week != 0)
                {
                    var result = ApplicationViewModel.modelDATABASE.Расписание.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().Where(p => p.Время == time).Where(p => p.День_недели == day_of_the_week).
                                                                                                                                        Where(p => p.Неделя == week);
                    if (result.Count() == 0)
                    {
                        try
                        {
                            int z = 0;
                            foreach (Расписание i in ApplicationViewModel.modelDATABASE.Расписание) z = i.Id;
                            //MessageBox.Show("Такого нету))))");
                            Расписание timetb = new Расписание();
                            timetb.Логин = ApplicationViewModel.Login_aut_user;
                            timetb.Время = time;
                            timetb.День_недели = day_of_the_week;
                            if (subject.Length > 40) subject = subject.Substring(0, 40);
                            timetb.Предмет = checked(subject + '.');
                            timetb.Неделя = week;
                            timetb.Id = z + 1;

                            ApplicationViewModel.modelDATABASE.Расписание.Add(timetb);
                            ApplicationViewModel.modelDATABASE.SaveChanges();
                            viewtable();
                            Subject = null;
                            StatusTimeTable = "Добавлено успешно";
                        }
                        catch (System.OverflowException e)
                        {

                            StatusTimeTable = "Error(ch):  " + e.ToString();
                        }
                    }
                    else
                    {
                        StatusTimeTable = "Error:в данное время есть предмет";
                    }
                }
                else
                {
                    StatusTimeTable = "Error:не выбрано/введено время |День недели \n|Неделя |Предмет";
                }
            }catch(Exception e)
            {
                StatusTimeTable = e.Message;
            }
        }

        public TimeTableR RetObg(string d_of_w, int n)
        {
            TimeTableR time1 = new TimeTableR();
            try
            {
             //   TimeTableR time1 = new TimeTableR();
                var result = ApplicationViewModel.modelDATABASE.Расписание.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().Where(p => p.День_недели == d_of_w).
                                                                                            Where(p => p.Неделя == n);
            //    TimeTableR time1 = new TimeTableR();
                time1.week = n;
                time1.day_of_the_week = d_of_w;
                foreach (Расписание i in result)
                {

                    if (i.Время == "8.00-9.35")
                    {
                        time1.time_and_sub8 += i.Предмет;
                    }
                    else if (i.Время == "9.50-11.25")
                    {
                        time1.time_and_sub9 += i.Предмет;
                    }
                    else if (i.Время == "11.40-13.15")
                    {
                        time1.time_and_sub11 += i.Предмет;
                    }
                    else if (i.Время == "13.50-15.25")
                    {
                        time1.time_and_sub13 += i.Предмет;
                    }
                    else if (i.Время == "15.40-17.15")
                    {
                        time1.time_and_sub15 += i.Предмет;
                    }
                    else if (i.Время == "17.30-19.05")
                    {
                        time1.time_and_sub17 += i.Предмет;
                    }
                    else if (i.Время == "19.20-20.55")
                    {
                        time1.time_and_sub19 += i.Предмет;
                    }

                    
                }
            }
            catch(Exception e)
            {

                StatusTimeTable = e.Message;
            }
            return time1;

        }

        
        public void viewtable()
        {
            timetable.Clear();
            timetable.Add(RetObg("понедельник", 1));
            timetable.Add(RetObg("понедельник", 2));

            timetable.Add(RetObg("вторник", 1));
            timetable.Add(RetObg("вторник", 2));

            timetable.Add(RetObg("среда", 1));
            timetable.Add(RetObg("среда", 2));

            timetable.Add(RetObg("четверг", 1));
            timetable.Add(RetObg("четверг", 2));

            timetable.Add(RetObg("пятница", 1));
            timetable.Add(RetObg("пятница", 2));

            timetable.Add(RetObg("суббота", 1));
            timetable.Add(RetObg("суббота", 2));

            timetable.Add(RetObg("воскресенье", 1));
            timetable.Add(RetObg("воскресенье", 2));
        }
        private string subjectCH;
        public string SubjectCH
        {
            set
            {
                subjectCH = value;
                RaisePropertyChanged("SubjectCH");
            }
            get
            {
                return subjectCH;
            }
        }
        private string timeCH;
        public string TimeCH
        {
            set
            {
                timeCH = value;
                RaisePropertyChanged("TimeCH");
            }
            get
            {
                return timeCH;
            }
        }
        public void SaveCh()
        {
            try
            {
                if (timeCH.Length != 0 && selectindexTimetable != -1 && subjectCH != null && timetable.Count > 0)
                {
                    //ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Modified;
                    //timetable[selectindexTimetable]
                    var result = ApplicationViewModel.modelDATABASE.Расписание.Where(p => p.Логин ==
                    ApplicationViewModel.Login_aut_user).ToList().
                    Where(p => p.День_недели == timetable[selectindexTimetable].day_of_the_week).
                    Where(p => p.Время == TimeCH).Where(p => p.Неделя == timetable[selectindexTimetable].week);
                    if (result.Count() != 0)
                    {
                        foreach (Расписание i in result)
                        {
                            if (subjectCH.Length >= 50) subjectCH = subjectCH.Substring(0, 49);
                            // MessageBox.Show(i.Неделя.ToString() + "   " + i.Предмет + " " + i.Логин);
                            i.Предмет = subjectCH + '.';
                            ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Modified;
                            //MessageBox.Show(i.Неделя.ToString() + "   " + i.Предмет);
                        }
                        ApplicationViewModel.modelDATABASE.SaveChanges();
                        viewtable();
                        subjectCH = null;
                        StatusTimeTable = "Изменено успешно";
                    }
                    else
                    {
                        StatusTimeTable = "Error:Нечего изменять";
                    }

                }
                else
                {
                    StatusTimeTable = "Error:Не указано время|не выбран день недели\n|Не указан предмет";
                }
            }
            catch(Exception e)
            {
                StatusTimeTable = "Error:" + e.Message;
            }
        }
        #endregion


        #region Реализация costs
        private string statusCosts;
        public string StatusCosts
        {
            set
            {
                statusCosts = value;
                RaisePropertyChanged("StatusCosts");
            }
            get
            {
                return statusCosts;
            }
        }
        public static ObservableCollection<Расходы> costs = new ObservableCollection<Расходы>();

        private int selectindexCosts;
        public int SelectIndexC
        {
            get { return selectindexCosts; }
            set
            {
                selectindexCosts = value;
               // MessageBox.Show(selectindexCosts.ToString());

            }
        }
        public ObservableCollection<Расходы> _Costs
        {
            get
            {
                return costs;
            }
            set { costs = value; }
        }

        public void openCost()
        {
            CostsWindow costsWindow = new CostsWindow();
            costsWindow.ShowDialog();
        }

        private string name_costs;
        public string Name_costs
        {
            set
            {
                name_costs = value;
                RaisePropertyChanged("Name_costs");
            }
            get
            {
                return name_costs;
            }
        }


        private int cost;
        public int Cost
        {
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");
            }
            get
            {
                return cost;
            }
        }

        private string periodicity_costs;
        public string Periodicity_costs
        {
            set
            {
                periodicity_costs = value;
               // MessageBox.Show(periodicity_costs);
                RaisePropertyChanged("Periodicity_costs");
            }
            get
            {
                return periodicity_costs;
            }
        }

        public void Addcosts()
        {
            try
            {
                var result = ApplicationViewModel.modelDATABASE.Расходы.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
                if (result.Count() <= 20)
                {
                    if (name_costs != null && periodicity_costs.Length != 0)
                    {
                        int z = 0;
                        foreach (Расходы i in ApplicationViewModel.modelDATABASE.Расходы) z = i.id;

                        Расходы расходы = new Расходы();
                        расходы.Логин = ApplicationViewModel.Login_aut_user;
                        try
                        {
                            if (cost < 0) cost = -cost;
                            расходы.Стоимость = cost;
                        }
                        catch (System.OverflowException e)
                        {
                            расходы.Стоимость = 0;
                            StatusCosts = "Error:" + e.ToString();
                        }
                        расходы.id = z + 1;
                        if (name_costs.Length >= 50) name_costs = name_costs.Substring(0, 49);
                        расходы.Группы_товара = name_costs + '.';
                        расходы.Периодиность = periodicity_costs;
                        ApplicationViewModel.modelDATABASE.Расходы.Add(расходы);
                        ApplicationViewModel.modelDATABASE.SaveChanges();
                        StatusCosts = "Добавлено успешно";
                        viewC();

                    }
                    else
                    {
                        StatusCosts = "Error:name costs|periodicity costs";
                    }
                }
                else
                {
                    StatusCosts = "Error:кол-во записей больше 20 ";
                }
            }
            catch(Exception e)
            {
                StatusCosts ="Error:"+ e.Message;
               // MessageBox.Show(e.Message);
            }
        }

        public void viewC()
        {
            try
            {
                costs.Clear();
                foreach (Расходы i in ApplicationViewModel.modelDATABASE.Расходы)
                {

                    if (i.Логин == ApplicationViewModel.Login_aut_user)
                    {
                        //System.Windows.MessageBox.Show(i.Логин);
                        costs.Add(i);
                    }

                }
            }catch(Exception e)
            {
                StatusCosts = "Error:" + e.Message;
            }

        }

        public void delC()
        {
            try
            {
                if (selectindexCosts != -1 && costs.Count>0)
                {
                    var result = ApplicationViewModel.modelDATABASE.Расходы.Where(p => p.Логин ==
                        ApplicationViewModel.Login_aut_user).ToList()
                        .Where
                        (p => p.id == costs[selectindexCosts].id);
                    foreach (Расходы i in result)
                    {
                        ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Deleted;
                    }
                    ApplicationViewModel.modelDATABASE.SaveChanges();
                    viewC();
                    StatusCosts = "del completed";
                }
                else
                {
                    StatusCosts = "Error:не выбран элемент|список пуст";
                }
            }
            catch(Exception e)
            {
                StatusCosts ="Error:"+ e.Message;
               // MessageBox.Show(e.Message);
            }
        }
        public void DelC_All()
        {
            try
            {
                var result = ApplicationViewModel.modelDATABASE.Расходы.Where(p => p.Логин ==
                        ApplicationViewModel.Login_aut_user);
                foreach (Расходы i in result)
                {
                    ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Deleted;
                }
                ApplicationViewModel.modelDATABASE.SaveChanges();
                StatusCosts = "del completed";
                viewC();
            }catch(Exception e)
            {
                StatusCosts = "Error:" + e.Message;
            }
        }


        private string name_costsC;
        public string Name_costsC
        {
            set
            {
                name_costsC = value;
                RaisePropertyChanged("Name_costsC");
            }
            get
            {
                return name_costsC;
            }
        }


        private int costC;
        public int CostC
        {
            set
            {
                costC = value;
                RaisePropertyChanged("CostC");
            }
            get
            {
                return costC;
            }
        }

        private string periodicity_costsC;
        public string Periodicity_costsC
        {
            set
            {
                periodicity_costsC = value;
               // MessageBox.Show(periodicity_costsC);
                RaisePropertyChanged("Periodicity_costsC");
            }
            get
            {
                return periodicity_costsC;
            }
        }

        public void ChC()
        {
            try
            {
                if (periodicity_costsC != null && name_costsC != null && selectindexCosts != -1 && costs.Count > 0)
                {
                    //System.Windows.MessageBox.Show("Периодичность-" + costs[selectindexCosts].Стоимость + "|Приоритет-" + costs[selectindexCosts].Периодиность + "|название-" +
                    //    costs[selectindexCosts].Группы_товара);
                    if (name_costsC.Length >= 50) name_costsC = name_costsC.Substring(0, 49);
                    costs[selectindexCosts].Группы_товара = name_costsC + '.';
                    try
                    {
                        if (costC < 0) costC = -costC;
                        costs[selectindexCosts].Стоимость = costC;
                    }
                    catch (System.OverflowException e)
                    {
                        costs[selectindexCosts].Стоимость = 0;
                        StatusCosts = "Error:" + e.ToString();
                    }
                    costs[selectindexCosts].Периодиность = periodicity_costsC;
                    ApplicationViewModel.modelDATABASE.Entry(costs[selectindexCosts]).State = EntityState.Modified;
                    ApplicationViewModel.modelDATABASE.SaveChanges();
                    viewC();
                    StatusCosts = "change completed";
                }
                else
                {
                    StatusCosts = "Error:не выбран элемент|список пуст|не заполнены поля";
                }
            }
            catch (Exception e)
            {
                StatusCosts = "Error:" + e.Message;
                // MessageBox.Show(e.Message);
            }
        }

        #endregion


        #region Реализация main
         ObservableCollection<task> TasksMain = new ObservableCollection<task>();
         ObservableCollection<TimeTableR> timetableMain = new ObservableCollection<TimeTableR>();
         ObservableCollection<Расходы> costsMain = new ObservableCollection<Расходы>();

        public ObservableCollection<task> _TasksMain
        {
            get
            {
                return TasksMain;
            }
            set { TasksMain = value; }
        }
        public ObservableCollection<TimeTableR> _timetableMain
        {
            get
            {
                return timetableMain;
            }
            set { timetableMain = value; }
        }
        public ObservableCollection<Расходы> _costsMain
        {
            get
            {
                return costsMain;
            }
            set { costsMain = value; }
        }
        public void ViewMain()
        {
            try
            {
                DateTime date1 = DateTime.Now;
                TasksMain.Clear();
                foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
                {

                    if (i.Логин == ApplicationViewModel.Login_aut_user && i.Дата == date1.ToString("dd.MM.yyyy"))
                    {
                        //System.Windows.MessageBox.Show(i.Логин);
                        TasksMain.Add(i);
                    }

                }

                timetableMain.Clear();
                if (DateTime.Now.DayOfWeek.ToString() == "Monday")
                {
                    timetableMain.Add(RetObg("понедельник", 1));
                    timetableMain.Add(RetObg("понедельник", 2));
                }
                else if (DateTime.Now.DayOfWeek.ToString() == "Tuesday")
                {
                    timetableMain.Add(RetObg("вторник", 1));
                    timetableMain.Add(RetObg("вторник", 2));
                }
                else if (DateTime.Now.DayOfWeek.ToString() == "Wednesday")
                {
                    timetableMain.Add(RetObg("среда", 1));
                    timetableMain.Add(RetObg("среда", 2));
                }
                else if (DateTime.Now.DayOfWeek.ToString() == "Thursday")
                {
                    timetableMain.Add(RetObg("четверг", 1));
                    timetableMain.Add(RetObg("четверг", 2));

                }
                else if (DateTime.Now.DayOfWeek.ToString() == "Friday")
                {
                    timetableMain.Add(RetObg("пятница", 1));
                    timetableMain.Add(RetObg("пятница", 2));

                }
                else if (DateTime.Now.DayOfWeek.ToString() == "Saturday")
                {
                    timetableMain.Add(RetObg("суббота", 1));
                    timetableMain.Add(RetObg("суббота", 2));
                }
                else if (DateTime.Now.DayOfWeek.ToString() == "Sunday")
                {
                    timetableMain.Add(RetObg("воскресенье", 1));
                    timetableMain.Add(RetObg("воскресенье", 2));
                }

                costsMain.Clear();
                foreach (Расходы i in ApplicationViewModel.modelDATABASE.Расходы)
                {

                    if (i.Логин == ApplicationViewModel.Login_aut_user)
                    {
                        //System.Windows.MessageBox.Show(i.Логин);
                        costsMain.Add(i);
                    }

                }



                StatusMain = "Completed";
            }catch(Exception e)
            {
                StatusMain = e.Message;
            }
        }

        private string statusMain;
        public string StatusMain
        {
            set
            {
                statusMain = value;
                RaisePropertyChanged("StatusMain");
            }
            get
            {
                return statusMain;
            }
        }

        private int selectindexMain;
        public int SelectIndexMain
        {
            get { return selectindexMain; }
            set
            {
                selectindexMain = value;
                //   MessageBox.Show(del_index.ToString());

            }
        }



        public void ChangeTaks()
        {
            try
            {
                if (selectindexMain != -1 && TasksMain.Count > 0)
                {
                    foreach (task i in ApplicationViewModel.modelDATABASE.tasks)
                    {
                        if (i.Логин == ApplicationViewModel.Login_aut_user && TasksMain[selectindexMain].id == i.id)
                        {


                            i.Status = 1;
                            ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Modified;
                        }

                    }
                    ApplicationViewModel.modelDATABASE.SaveChanges();
                    ViewMain();
                    StatusMain = "Completed";
                }
                else
                {
                    StatusMain = "Error:список пуст|\nне выбран эл.";
                }
            }catch(Exception e)
            {
                StatusMain = e.Message;
            }
        }


        //void Write()
        //{
        //    string writePath = @"tasksend.txt";
        //    DateTime date1 = DateTime.Now;
        //    try
        //    {
        //        string res;
        //        var result = ApplicationViewModel.modelDATABASE.tasks.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().
        //            Where(p => p.Дата == date1.ToString("dd.MM.yyyy"));
        //      //  StreamWriter sw;
        //        //using (StreamWriter sw = new StreamWriter(writePath,
        //        //                        false, System.Text.Encoding.Default))
        //        //{

        //        //    foreach (task i in result)
        //        //    {

        //        //        if (i.Status == 1) res = "Выполнена";
        //        //        else res = "Не выполнена";
        //        //        sw.Write("Название задачи->" + i.Название + '\n' +
        //        //            "Приоритет->" + i.Приоритет.ToString() + '\n' +
        //        //            "Периодичность" + i.Периодичность + '\n' +
        //        //            "Полное описание->" + i.Полное_описание + '\n' +
        //        //            "Статус->" + res + '\n' +
        //        //            "-------------------------------------------" + '\n'
        //        //            );


        //        //    }
        //        //}


        //        Stream myStream;
        //        using (myStream = File.Open(writePath, FileMode.Create, FileAccess.Write))
        //        {
        //            StreamWriter sw = new StreamWriter(myStream);
        //            foreach (task i in result)
        //            {

        //                if (i.Status == 1) res = "Выполнена";
        //                else res = "Не выполнена";
        //                sw.Write("Название задачи->" + i.Название + '\n' +
        //                    "Приоритет->" + i.Приоритет.ToString() + '\n' +
        //                    "Периодичность" + i.Периодичность + '\n' +
        //                    "Полное описание->" + i.Полное_описание + '\n' +
        //                    "Статус->" + res + '\n' +
        //                    "-------------------------------------------" + '\n'
        //                    );


        //            }
        //        }
        //        MessageBox.Show("ЗАписано");
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}



        public void SendMesTASK()
        {
            try
            {
                StatusMain = "Wait...";
                DateTime date1 = DateTime.Now;
                string res = "";
                string txt = "";
                var result = ApplicationViewModel.modelDATABASE.tasks.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().
                    Where(p => p.Дата == date1.ToString("dd.MM.yyyy"));
                var result2 = ApplicationViewModel.modelDATABASE.Users.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
                foreach (task i in result)
                {

                    if (i.Status == 1) res = "Выполнена";
                    else res = "Не выполнена";
                    txt += ("Название задачи-" + i.Название + "<br>" +
                         "Приоритет-" + i.Приоритет.ToString() + "<br>" +
                         "Периодичность-" + i.Периодичность + "<br>" +
                         "Полное описание-" + i.Полное_описание + "<br>" +
                          res + "<br>" +
                         "-------------------------------------------" + "<br>"
                         );


                }
                
                string e_mail="";
                foreach(User i in result2) e_mail=i.e_maeil;

                    // отправитель - устанавливаем адрес и отображаемое в письме имя kirya.bolvako@gmail.com
                    MailAddress from = new MailAddress("course.net.fit@gmail.com", "Admin");
                // кому отправляем
                MailAddress to = new MailAddress(e_mail);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Задачи";
                // текст письма
                m.Body = "<h2>Задачи на Сегодня</h2> < br > ";
                m.Body = txt;
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("course.net.fit@gmail.com", "Kirill_super123");
                smtp.EnableSsl = true;
                smtp.Send(m);
                StatusMain = "Если сообщение не при\nшло проверьте e-maeil";
            }
            catch(Exception e)
            {
                StatusMain = "Error:" + e.Message;
            }

            // File.Delete(@"tasksend.txt");
            // SendMail("smtp.gmail.com", "kirya.bolvako@gmail.com", "Kirill_super123", "course.net.fit@gmail.com", "Тема письма", "Тело письма");
        }


        public void SendMesTB()
        {
            try
            {
                string week = "";
                if (DateTime.Now.DayOfWeek.ToString() == "Monday") week = "понедельник";
                else if (DateTime.Now.DayOfWeek.ToString() == "Tuesday") week = "вторник";
                else if (DateTime.Now.DayOfWeek.ToString() == "Wednesday") week = "среда";
                else if (DateTime.Now.DayOfWeek.ToString() == "Thursday") week = "четверг";
                else if (DateTime.Now.DayOfWeek.ToString() == "Friday") week = "пятница";
                else if (DateTime.Now.DayOfWeek.ToString() == "Saturday") week = "суббота";
                else if (DateTime.Now.DayOfWeek.ToString() == "Sunday") week = "воскресенье";

                DateTime date1 = DateTime.Now;
                string txt = "";
                var result = ApplicationViewModel.modelDATABASE.Расписание.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().
                    Where(p => p.День_недели == week);
                var result2 = ApplicationViewModel.modelDATABASE.Users.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
                TimeTableR tableR1 = RetObg(week, 1);
                TimeTableR tableR2 = RetObg(week, 2);

                txt += "<h3>День недели " + tableR1.day_of_the_week + "</h3>" + "<br>";
                txt += "<h3>Неделя " + tableR1.week + "</h3>" + "<br>";
                txt += tableR1.time_and_sub8 + "<br>";
                txt += tableR1.time_and_sub9 + "<br>";
                txt += tableR1.time_and_sub11 + "<br>";
                txt += tableR1.time_and_sub13 + "<br>";
                txt += tableR1.time_and_sub15 + "<br>";
                txt += tableR1.time_and_sub17 + "<br>";
                txt += tableR1.time_and_sub19 + "<br>";
                txt += "<h3>Неделя " + tableR2.week + "</h3>" + "<br>";
                txt += tableR2.time_and_sub8 + "<br>";
                txt += tableR2.time_and_sub9 + "<br>";
                txt += tableR2.time_and_sub11 + "<br>";
                txt += tableR2.time_and_sub13 + "<br>";
                txt += tableR2.time_and_sub15 + "<br>";
                txt += tableR2.time_and_sub17 + "<br>";
                txt += tableR2.time_and_sub19 + "<br>";
                StatusMain = "Wait...";
                string e_mail = "";
                foreach (User i in result2) e_mail = i.e_maeil;

                // отправитель - устанавливаем адрес и отображаемое в письме имя kirya.bolvako@gmail.com
                MailAddress from = new MailAddress("course.net.fit@gmail.com", "Admin");
                // кому отправляем
                MailAddress to = new MailAddress(e_mail);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Расписание";
                // текст письма
                m.Body = "<h2>Расписание на Сегодня</h2> < br > ";
                m.Body = txt;
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("course.net.fit@gmail.com", "Kirill_super123");
                smtp.EnableSsl = true;
                smtp.Send(m);
                StatusMain = "Если сообщение не при\nшло проверьте e-maeil";
                txt = "";
            }catch(Exception e)
            {
                StatusMain = "Error:" + e.Message;
            }
        }

        //<TextBlock Text = "1 раз в неделю" />
        //    < TextBlock Text="2 раза в неделю"/>
        //    <TextBlock Text = "3 раза в неделю" />
        //    < TextBlock Text="1 раз в месяц"/>
        //    <TextBlock Text = "2 раза в месяц" />
        //    < TextBlock Text="3 раза в месяц"/>



        ObservableCollection<User> UserA = new ObservableCollection<User>();

        public ObservableCollection<User> _UserA
        {
            get
            {
                return UserA;
            }
            set { UserA = value; }
        }
        public void SendMescosts()
        {
            try
            {
                string txt = "";
            string txt_tes = "";
            double res = 0;
            var result2 = ApplicationViewModel.modelDATABASE.Users.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
            var result = ApplicationViewModel.modelDATABASE.Расходы.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
            txt += "<h3>Список ваших товаров за месяц<h3><br>";
            txt += "В среднем бралось 4.3482 недель в месяце";
                txt += "<ul>";
            foreach (Расходы i in result)
            {
                txt += "<li>" + i.Группы_товара +"  Цена->"+i.Стоимость.ToString()+ "<li>";
                if(i.Периодиность== "1 раз в неделю")
                {
                    if (i.Стоимость > 0) res = res + i.Стоимость * 4.3482;
                }
                else if (i.Периодиность == "2 раза в неделю")
                {
                    if (i.Стоимость > 0) res = res + 2*i.Стоимость * 4.3482;
                }
                else if (i.Периодиность == "3 раза в неделю")
                {
                    if (i.Стоимость > 0) res = res + 3*i.Стоимость * 4.3482;
                }
                else if (i.Периодиность == "1 раз в месяц")
                {
                    if (i.Стоимость > 0) res = res + i.Стоимость ;
                }
                else if (i.Периодиность == "2 раза в месяц")
                {
                    if (i.Стоимость > 0) res = res + 2*i.Стоимость ;
                }
                else if (i.Периодиность == "3 раза в месяц")
                {
                    if (i.Стоимость > 0) res = res + 3*i.Стоимость;
                }
            }
                txt += "</ul>";
            txt_tes = "<h4>В месяц выходит  " + res.ToString() + "</h4><br>";
            string e_mail = "";
            foreach (User i in result2) e_mail = i.e_maeil;

            // отправитель - устанавливаем адрес и отображаемое в письме имя kirya.bolvako@gmail.com
            MailAddress from = new MailAddress("course.net.fit@gmail.com", "Admin");
            // кому отправляем
            MailAddress to = new MailAddress(e_mail);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Расходы";
            // текст письма
            m.Body = "<h2>Расчёт расходов за месяц</h2> < br > ";
            m.Body = txt;
                m.Body += txt_tes;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("course.net.fit@gmail.com", "Kirill_super123");
            smtp.EnableSsl = true;
            smtp.Send(m);
            StatusMain = "Если сообщение не при\nшло проверьте e-maeil";
            }
            catch (Exception e)
            {
                StatusMain = "Error:" + e.Message;
            }
        }
        private string e_mail_for_reg1;
        public string E_mail_for_reg1
        {
            set
            {
                e_mail_for_reg1 = value;
                RaisePropertyChanged("E_mail_for_reg1");
            }
        }

        private string statusAdmin;
        public string StatusAdmin
        {
            set
            {
                statusAdmin = value;
                RaisePropertyChanged("StatusAdmin");
            }
            get
            {
                return statusAdmin;
            }
        }

        private int selectindexAdmin;
        public int SelectIndexAdmin
        {
            get { return selectindexAdmin; }
            set
            {
                selectindexAdmin = value;
                //   MessageBox.Show(del_index.ToString());

            }
        }
        public void Admin()
        {
            try
            {
                String login="";
                string regex_for_email = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
                if (e_mail_for_reg1 != null && selectindexAdmin != -1)
                {
                    int m = 0;
                    if (Regex.IsMatch(e_mail_for_reg1, regex_for_email, RegexOptions.IgnoreCase))
                    {
                        foreach (User i in ApplicationViewModel.modelDATABASE.Users)
                        {
                            if (e_mail_for_reg1 == i.e_maeil)
                            {
                                m = 1;
                                StatusAdmin = "Error: Пользователь с таким email уже существует";
                                break;
                            }

                        }

                    }
                    else
                    {
                        m = 1;
                        StatusAdmin = "Error:Email не подтвержден";
                    }
                    if (m != 1)
                    {
                        //MessageBox.Show(selectindexAdmin.ToString());
                       // MessageBox.Show(UserA[selectindexAdmin].Логин);
                        var result2 = ApplicationViewModel.modelDATABASE.Users;
              
                        foreach(User i in result2)
                        {
                            if (i.Логин == UserA[selectindexAdmin].Логин)
                            {
                                i.e_maeil = e_mail_for_reg1;
                                ApplicationViewModel.modelDATABASE.Entry(i).State = EntityState.Modified;
                                break;
                            }
                        }
                        ApplicationViewModel.modelDATABASE.SaveChanges();
                        viewAdmin();
                        StatusAdmin = "Completed";
                    }

                }
                else
                {
                    StatusAdmin = "Error:Email не введён |не выбран элемент из списка|\nсписок пуст";
                }
            }
            catch(Exception e)
            {
                StatusAdmin = "Error:"+e.Message;
            }
        }
        public void openAdmin()
        {
            try
            {
                bool fl = false;
                var result2 = ApplicationViewModel.modelDATABASE.Users.Where(p => p.Логин == ApplicationViewModel.Login_aut_user);
                foreach (User i in result2)
                {
                    if (i.Роль == "admin") fl = true;
                }
                if (fl)
                {
                    Admin ad = new Admin();
                    ad.ShowDialog();
                }
                else
                {
                    StatusMain = "Недостаточно прав";
                }
            }catch(Exception e)
            {
                StatusMain = e.Message;
            }
        }

        public void viewAdmin()
        {
            try
            {
                var result2 = unitofwork.Users.GetAll();
                UserA.Clear();
                foreach (User i in result2)
                {
                    UserA.Add(i);
                }
            }catch(Exception e)
            {
                StatusMain = e.Message;
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
