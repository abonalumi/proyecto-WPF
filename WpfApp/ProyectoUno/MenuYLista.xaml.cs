using ProyectoUno.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoUno
{
    /// <summary>
    /// Interaction logic for MenuYLista.xaml
    /// </summary>
    public partial class MenuYLista : Page
    {
        public MenuYLista()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            List<PersonViewModel> PersonList = new List<PersonViewModel>();
            using (Models.WPF_AppDBEntities db = new Models.WPF_AppDBEntities())
            {
                PersonList = (
                        from person in db.Persons
                        select new PersonViewModel
                        {
                            Name = person.Name,
                            Age = person.Age,
                            Id = person.Id
                        }
                    ).ToList();
            }

            PersonDG.ItemsSource = PersonList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StaticMainFrame.Content = new Formulario();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            using (Models.WPF_AppDBEntities db = new Models.WPF_AppDBEntities())
            {
                var persona = db.Persons.Find(Id);
                db.Persons.Remove(persona);
                db.SaveChanges();
            }

            Refresh();
        }

        private void Button_Editar_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            Formulario formulario = new Formulario(Id);

            MainWindow.StaticMainFrame.Content = formulario;
        }

    }


}
