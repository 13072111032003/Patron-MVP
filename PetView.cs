using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatrónMVP.Views
{
    public partial class PetView : Form, IPetView
    {
        //Fields
        private string Mensaje;
        private bool Correcto;
        private bool Editar;

        //Constructor
        public PetView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPagePetDetail);
            btnClose.Click += delegate { this.Close(); };
        }

        event EventHandler IPetView.Editar
        {
            add
            {((IPetView)instance).Editar += value;}
            remove
            {((IPetView)instance).Editar -= value;}
        }

        private void AssociateAndRaiseViewEvents()
        {
            //Search
            btnSearch.Click += delegate {Busca?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
              {
                  if (e.KeyCode == Keys.Enter)
                      Busca?.Invoke(this, EventArgs.Empty);
              };
            //Add new
            btnAddNew.Click += delegate
            {
                Añadir?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetList);
                tabControl1.TabPages.Add(tabPagePetDetail);
                tabPagePetDetail.Text = "Añadir nueva mascota";
            };
            //Editar
            btnEdit.Click += delegate
            {
                Editación?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetList);
                tabControl1.TabPages.Add(tabPagePetDetail);
                tabPagePetDetail.Text = "Editar mascoja";
            };
            //Save changes
            btnSave.Click += delegate
            {
                Guardar?.Invoke(this, EventArgs.Empty);
                if (Correcto)
                {
                    tabControl1.TabPages.Remove(tabPagePetDetail);
                    tabControl1.TabPages.Add(tabPagePetList);
                }
                MessageBox.Show(Mensaje);
            };
            //Cancelar
            btnCancel.Click += delegate
            {
                Cancelar?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetDetail);
                tabControl1.TabPages.Add(tabPagePetList);
            };
            //Borrar
            btnDelete.Click += delegate
            {               
                var result = MessageBox.Show("Are you sure you want to delete the selected pet?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Borrar?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Mensaje);
                }
            };
        }

        //Propiedades
        public string PetId
        {
            get { return txtPetId.Text; }
            set { txtPetId.Text = value; }
        }

        public string PetNombre
        {
            get { return txtPetName.Text; }
            set { txtPetName.Text = value; }
        }

        public string PetTipo
        {
            get { return txtPetType.Text; }
            set { txtPetType.Text = value; }
        }

        public string PetColor
        {
            get { return txtPetColour.Text; }
            set { txtPetColour.Text = value; }
        }

        public string Buscar
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public bool Edita
        {
            get { return Editar; }
            set { Editar = value; }
        }
        public bool correcto
        {
            get { return Correcto; }
            set { Correcto = value; }
        }
        public string mensaje
        {
            get { return Mensaje; }
            set { Mensaje = value; }
        }

        public bool Corecto { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IPetView.Mensaje { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //Events
        public event EventHandler Busca;
        public event EventHandler Añadir;
        public event EventHandler Editación;
        public event EventHandler Borrar;
        public event EventHandler Guardar;
        public event EventHandler Cancelar;

        //Methods
        public void SetPetListBindingSource(BindingSource petList)
        {
            dataGridView.DataSource = petList;
        }

        //Singleton pattern (Open a single form instance)
        private static PetView instance;
        public static PetView GetInstace(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PetView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
