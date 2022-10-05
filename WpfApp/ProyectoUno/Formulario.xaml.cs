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
    /// Interaction logic for Formulario.xaml
    /// </summary>
    public partial class Formulario : Page
    {
        public int Id = 0;
        public Formulario(int id=0)
        {
            InitializeComponent();
            this.Id = id;
            if (Id != 0) {
                using (Models.WPF_AppDBEntities db = new Models.WPF_AppDBEntities())
                {
                    var persona = db.Persons.Find(Id);
                    tbNombre.Text = persona.Name;
                    tbEdad.Text = persona.Age.ToString();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Id == 0)
            {
                using (Models.WPF_AppDBEntities db = new Models.WPF_AppDBEntities())
                {
                    var persona = new Models.Persons();
                    persona.Name = tbNombre.Text;
                    persona.Age = int.Parse(tbEdad.Text);

                    db.Persons.Add(persona);
                    db.SaveChanges();

                    MainWindow.StaticMainFrame.Content = new MenuYLista();
                }
            }
            else
            {
                using (Models.WPF_AppDBEntities db = new Models.WPF_AppDBEntities())
                {
                    var persona = db.Persons.Find(Id);
                    persona.Name = tbNombre.Text;
                    persona.Age = int.Parse(tbEdad.Text);

                    db.Entry(persona).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    MainWindow.StaticMainFrame.Content = new MenuYLista();
                }
            }
        }
    }
}
