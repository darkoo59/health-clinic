using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace Sims_Hospital_Zdravo.View.Secretary.Translation
{
    class TranslationSource : INotifyPropertyChanged
    {
        private static readonly TranslationSource instance = new TranslationSource();

        public static TranslationSource Instance
        {
            get { return instance; }
        }

        private readonly ResourceManager resManager = Properties.Resources.ResourceManager;
        private CultureInfo currentCulture = null;

        public string this[string key]
        {
            get
            {
                string retVal = this.resManager.GetString(key, this.currentCulture);
                return retVal;
            }
        }

        private void applyResources(ComponentResourceManager manager, System.Windows.Forms.Control.ControlCollection ctls)
        {
            foreach (System.Windows.Forms.Control ctl in ctls)
            {
                manager.ApplyResources(ctl, ctl.Name);
                applyResources(manager, ctl.Controls);
            }
        }

        private void localizeForm(Form frm)
        {
            var manager = new ComponentResourceManager(frm.GetType());
            manager.ApplyResources(frm, "$this");
            applyResources(manager, frm.Controls);
        }

        public void ChangeLanguage(String language)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            foreach (Form frm in System.Windows.Forms.Application.OpenForms)
            {
                localizeForm(frm);
            }
        }

        public CultureInfo CurrentCulture
        {
            get { return this.currentCulture; }
            set
            {
                if (this.currentCulture != value)
                {
                    this.currentCulture = value;
                    var @event = this.PropertyChanged;
                    if (@event != null)
                    {
                        @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LocExtension : System.Windows.Data.Binding
    {
        public LocExtension(string name)
            : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationSource.Instance;
        }
    }
}
