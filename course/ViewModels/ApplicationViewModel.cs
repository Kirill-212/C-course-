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
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace course.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
       
        public EntranceCommand entrance { get; private set; }
        public CheckforValidRegCommand checkfor { get; private set; }



        public ViewCommand viewcommand { get; private set; }
        public AddTaskCommand addTask { get; private set; }
        public DelCommand delCommand { get; private set; }
        public ChangeCommand ChangeCommand { get; private set; } 
        public SaveCommand saveCommand { get; private set; }




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
        public ApplicationViewModel()
        {
            entrance = new EntranceCommand(Entrance);
            checkfor = new CheckforValidRegCommand(checkforvalidReg);
            addTask = new AddTaskCommand(AddTask);
            viewcommand = new ViewCommand(Viewcommand);
            delCommand = new DelCommand(Del);
            ChangeCommand = new ChangeCommand(StatusTasks);
            saveCommand = new SaveCommand(Save);

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
            
        }



       
        #region Для входа 
        public void Entrance()//вход
        {
            int m = 1;
            //MainWindow.modelDATABASE.Users.;
            //var result = MainWindow.modelDATABASE.Users.Where(p => p.Логин == login).Where(p => p.Пароль == password);
            //if (result != null)
            //{
            //    m=0;
            //}
            foreach (User i in MainWindow.modelDATABASE.Users)
            {
                if (login == i.Логин)
                {
                    if (password == i.Пароль)
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
        private int password;
        public int Password
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
                int m = 0;
                if (Regex.IsMatch(e_mail_for_reg, regex_for_email, RegexOptions.IgnoreCase))
                {
                    foreach (User i in MainWindow.modelDATABASE.Users)
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
                    Status= "Error:Email не подтвержден";
                }
                if(Regex.IsMatch(login_for_reg, regex_for_login, RegexOptions.IgnoreCase))
                {
                    foreach (User i in MainWindow.modelDATABASE.Users)
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
                    user.Пароль = password_for_reg;
                    Status = "Successfull";
                    E_mail_for_reg = "";
                    Login_for_reg = "";
                    Password_for_reg = default;
                    MainWindow.modelDATABASE.Users.Add(user);
                    MainWindow.modelDATABASE.SaveChanges();
                    System.Windows.MessageBox.Show("e_mail  " + user.e_maeil + "|Login  " + user.Логин + "|Password  " + user.Пароль.ToString());

                    
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
        private int password_for_reg;
        public int Password_for_reg
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
                
                if ( date.Length!=0)
                {
                    int z = 0;
                    foreach (task i in MainWindow.modelDATABASE.tasks) z = i.id;
                        //MessageBox.Show(date);
                        foreach (Date i in MainWindow.modelDATABASE.Dates)
                    {
                      //   MessageBox.Show(i.Дата.ToString() + "    " + date);
                        if (i.Дата == date  )
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
                        MainWindow.modelDATABASE.Dates.Add(date_tasks);
                        MainWindow.modelDATABASE.SaveChanges();
                       // MessageBox.Show(date_tasks.Дата.ToString() + "||||" + date);
                    }
                    if ( name==null|| name=="")
                    {
                        StatusAddTask = "Error:name";
                    }
                    else if ( periodicity == null || periodicity == "")
                    {
                        StatusAddTask = "Error:periodicity";
                    }
                    else if (full_name == null || full_name == "")
                    {
                        StatusAddTask = "Error:full_name";
                    }
                    else if ( priorety.ToString() == null || priorety.ToString() == "" || priorety==0)
                    {
                        StatusAddTask = "Error:priorety";
                    }
                    else
                    {
                        task task_ = new task();
                        task_.Название = name+'.';
                        task_.Периодичность = periodicity;
                        task_.Полное_описание = full_name+'.';
                        task_.Дата = date;
                        task_.Приоритет = priorety;
                        task_.Status = 0;
                        task_.id=z + 1;
                        task_.Логин = ApplicationViewModel.Login_aut_user;
                        MainWindow.modelDATABASE.tasks.Add(task_);
                        MainWindow.modelDATABASE.SaveChanges();
                        StatusAddTask = "operation completed successfully";
                        System.Windows.MessageBox.Show(priorety + " приоритет"+Environment.NewLine+ periodicity + " периодичность|"+date+"|"+name+"|"+full_name+"|");
                        Viewcommand();
                        Name = null;
                        Full_name = null;
                    }
                    
                }
                else
                {
                    StatusAddTask = "Error:date";
                }
                


            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                StatusAddTask = e.Message;
            }
            
        }



        public void Viewcommand()
        {

            TasksObserv.Clear();
            foreach (task i in MainWindow.modelDATABASE.tasks)
            {
                
                if (i.Логин == ApplicationViewModel.Login_aut_user)
                {
                    //System.Windows.MessageBox.Show(i.Логин);
                    TasksObserv.Add(i);
                }
               
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

                        foreach (task i in MainWindow.modelDATABASE.tasks)
                        {
                            if (i.Логин == ApplicationViewModel.Login_aut_user)
                            {
                                MainWindow.modelDATABASE.Entry(i).State = EntityState.Deleted;

                            }

                        }
                        MainWindow.modelDATABASE.SaveChanges();
                        StatusAddTask = "del completed successfully";
                    }
                    else if (delIndex == 1)
                    {
                        if (selectindex != -1)
                        {
                           // System.Windows.MessageBox.Show("1");
                            foreach (task i in MainWindow.modelDATABASE.tasks)
                            {
                                if (i.Логин == ApplicationViewModel.Login_aut_user && TasksObserv[selectindex] == i)
                                {
                                    MainWindow.modelDATABASE.Entry(i).State = EntityState.Deleted;

                                }

                            }
                            MainWindow.modelDATABASE.SaveChanges();
                            StatusAddTask = "del completed successfully";
                        }
                        else
                        {
                            StatusAddTask = "Error:удаление неозможно т.к. не выбран элемент";
                        }

                    }
                    else if (delIndex == 2)
                    {
                        foreach (task i in MainWindow.modelDATABASE.tasks)
                        {
                            //System.Windows.MessageBox.Show(DateTime.Today.Year.ToString() + "." + DateTime.Today.Month.ToString() +
                            //    "." + DateTime.Today.ToString()
                            //     + "----" + i.Дата);
                            if (i.Логин == ApplicationViewModel.Login_aut_user && !(DateTime.Today.ToString() == i.Дата))
                            {
                                System.Windows.MessageBox.Show("удаляем" + i.Дата.ToString());
                                MainWindow.modelDATABASE.Entry(i).State = EntityState.Deleted;

                            }

                        }
                        MainWindow.modelDATABASE.SaveChanges();
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
            if (selectindex != -1 && TasksObserv.Count > 0)
            {
                foreach (task i in MainWindow.modelDATABASE.tasks)
                {
                    if (i.Логин == ApplicationViewModel.Login_aut_user && TasksObserv[selectindex].id == i.id)
                    {


                        i.Status = 1;
                        MainWindow.modelDATABASE.Entry(i).State = EntityState.Modified;
                    }

                }
                MainWindow.modelDATABASE.SaveChanges();
                Viewcommand();
                StatusAddTask = "task completed successfully";
            }
            else
            {
                StatusAddTask = "Error:Не выбрали задание,которое надо выполнить\n|список задач пуст";
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
            if (full_name1 != null && name1 != null && selectindex!=-1 && TasksObserv.Count>0)
            {
                
                //System.Windows.MessageBox.Show("Периодичность-" + TasksObserv[selectindex].Периодичность + "|Приоритет-" + TasksObserv[selectindex].Приоритет + "|название-" +
                //    TasksObserv[selectindex].Название);
                TasksObserv[selectindex].Название = name1+'.';
                TasksObserv[selectindex].Полное_описание = full_name1+'.';
                MainWindow.modelDATABASE.Entry(TasksObserv[selectindex]).State = EntityState.Modified;
                MainWindow.modelDATABASE.SaveChanges();
                Viewcommand();
                Full_name1 = "";
                Name1 = "";
                StatusAddTask = "change completed";
            }
            else
            {
                StatusAddTask = "Error:не заполнены все поля|не выбрали элемент\n|список задач пуст";
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
            var result = MainWindow.modelDATABASE.Расписание.Where(p => p.Логин ==
                   ApplicationViewModel.Login_aut_user);
            foreach (Расписание i in result)
            {
                MainWindow.modelDATABASE.Entry(i).State = EntityState.Deleted;
            }
            MainWindow.modelDATABASE.SaveChanges();
            viewtable();
            StatusTimeTable = "clear completed";
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
            if (time.Length != 0 && day_of_the_week.Length != 0 && subject != null && week != 0)
            {
                var result = MainWindow.modelDATABASE.Расписание.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().Where(p => p.Время == time).Where(p => p.День_недели == day_of_the_week).
                                                                                                                                    Where(p => p.Неделя == week);
                if (result.Count() == 0)
                {

                    int z = 0;
                    foreach (Расписание i in MainWindow.modelDATABASE.Расписание) z = i.Id;
                    //MessageBox.Show("Такого нету))))");
                    Расписание timetb = new Расписание();
                    timetb.Логин = ApplicationViewModel.Login_aut_user;
                    timetb.Время = time;
                    timetb.День_недели = day_of_the_week;
                    timetb.Предмет = subject+'.';
                    timetb.Неделя = week;
                    timetb.Id = z + 1;

                    MainWindow.modelDATABASE.Расписание.Add(timetb);
                    MainWindow.modelDATABASE.SaveChanges();
                    viewtable();
                    Subject = null;
                    StatusTimeTable = "Добавлено успешно";
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
        }

        public TimeTableR RetObg(string d_of_w, int n)
        {
            var result = MainWindow.modelDATABASE.Расписание.Where(p => p.Логин == ApplicationViewModel.Login_aut_user).ToList().Where(p => p.День_недели == d_of_w).
                                                                                        Where(p => p.Неделя == n);
            TimeTableR time1 = new TimeTableR();
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
            if (timeCH.Length!=0 && selectindexTimetable!=-1 && subjectCH != null && timetable.Count>0)
            {            
                    //MainWindow.modelDATABASE.Entry(i).State = EntityState.Modified;
                    //timetable[selectindexTimetable]
                    var result = MainWindow.modelDATABASE.Расписание.Where(p => p.Логин ==
                    ApplicationViewModel.Login_aut_user).ToList().
                    Where(p => p.День_недели == timetable[selectindexTimetable].day_of_the_week).
                    Where(p => p.Время == TimeCH).Where(p => p.Неделя == timetable[selectindexTimetable].week);
                if (result.Count() != 0)
                {
                    foreach (Расписание i in result)
                    {
                       // MessageBox.Show(i.Неделя.ToString() + "   " + i.Предмет + " " + i.Логин);
                        i.Предмет = subjectCH+'.';
                        MainWindow.modelDATABASE.Entry(i).State = EntityState.Modified;
                        //MessageBox.Show(i.Неделя.ToString() + "   " + i.Предмет);
                    }
                    MainWindow.modelDATABASE.SaveChanges();
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


        private decimal cost;
        public decimal Cost
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
                if (name_costs != null && periodicity_costs.Length != 0)
                {
                    int z = 0;
                    foreach (Расходы i in MainWindow.modelDATABASE.Расходы) z = i.id;
                    if (cost < 0) cost = -cost;
                    Расходы расходы = new Расходы();
                    расходы.Логин = ApplicationViewModel.Login_aut_user;
                    расходы.Стоимость = cost;
                    расходы.id = z + 1;
                    расходы.Группы_товара = name_costs+'.';
                    расходы.Периодиность = periodicity_costs;
                    MainWindow.modelDATABASE.Расходы.Add(расходы);
                    MainWindow.modelDATABASE.SaveChanges();
                    StatusCosts = "Добавлено успешно";
                    viewC();
                }
                else
                {
                    StatusCosts = "Error:name costs|periodicity costs";
                }
            }
            catch(Exception e)
            {
                StatusCosts = e.Message;
               // MessageBox.Show(e.Message);
            }
        }

        public void viewC()
        {
            costs.Clear();
            foreach (Расходы i in MainWindow.modelDATABASE.Расходы)
            {

                if (i.Логин == ApplicationViewModel.Login_aut_user)
                {
                    //System.Windows.MessageBox.Show(i.Логин);
                    costs.Add(i);
                }

            }

        }

        public void delC()
        {
            try
            {
                if (selectindexCosts != -1 && costs.Count>0)
                {
                    var result = MainWindow.modelDATABASE.Расходы.Where(p => p.Логин ==
                        ApplicationViewModel.Login_aut_user).ToList()
                        .Where
                        (p => p.id == costs[selectindexCosts].id);
                    foreach (Расходы i in result)
                    {
                        MainWindow.modelDATABASE.Entry(i).State = EntityState.Deleted;
                    }
                    MainWindow.modelDATABASE.SaveChanges();
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
                StatusCosts = e.Message;
               // MessageBox.Show(e.Message);
            }
        }
        public void DelC_All()
        {
            var result = MainWindow.modelDATABASE.Расходы.Where(p => p.Логин ==
                    ApplicationViewModel.Login_aut_user);
            foreach (Расходы i in result)
            {
                MainWindow.modelDATABASE.Entry(i).State = EntityState.Deleted;
            }
            MainWindow.modelDATABASE.SaveChanges();
            StatusCosts = "del completed";
            viewC();
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


        private decimal costC;
        public decimal CostC
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
            if (periodicity_costsC != null && name_costsC != null && selectindexCosts != -1 && costs.Count > 0)
            {
                //System.Windows.MessageBox.Show("Периодичность-" + costs[selectindexCosts].Стоимость + "|Приоритет-" + costs[selectindexCosts].Периодиность + "|название-" +
                //    costs[selectindexCosts].Группы_товара);
                costs[selectindexCosts].Группы_товара = name_costsC+'.';
                costs[selectindexCosts].Стоимость = costC;
                costs[selectindexCosts].Периодиность = periodicity_costsC;
                MainWindow.modelDATABASE.Entry(costs[selectindexCosts]).State = EntityState.Modified;
                MainWindow.modelDATABASE.SaveChanges();
                viewC();
                StatusCosts = "change completed";
            }
            else
            {
                StatusCosts = "Error:не выбран элемент|список пуст|не заполнены поля";
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
            DateTime date1 = DateTime.Now;
            TasksMain.Clear();
            foreach (task i in MainWindow.modelDATABASE.tasks)
            {

                if (i.Логин == ApplicationViewModel.Login_aut_user && i.Дата ==date1.ToString("dd.MM.yyyy"))
                {
                    //System.Windows.MessageBox.Show(i.Логин);
                    TasksMain.Add(i);
                }

            }

            timetableMain.Clear();
            if (DateTime.Now.DayOfWeek.ToString()== "Monday")
            {
                timetableMain.Add(RetObg("понедельник", 1));
                timetableMain.Add(RetObg("понедельник", 2));
            }
            else if(DateTime.Now.DayOfWeek.ToString() == "Tuesday")
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
            foreach (Расходы i in MainWindow.modelDATABASE.Расходы)
            {

                if (i.Логин == ApplicationViewModel.Login_aut_user)
                {
                    //System.Windows.MessageBox.Show(i.Логин);
                    costsMain.Add(i);
                }

            }



            StatusMain = "Completed";

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
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
