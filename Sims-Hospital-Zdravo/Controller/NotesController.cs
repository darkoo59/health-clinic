using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Controller
{
    public class NotesController
    {
        private NotesService _notesService;
        public NotesController()
        {
            _notesService = new NotesService();
        }
        public void Create(Notes notes)
        {
            _notesService.Create(notes);
        }
        public List<Notes> FindAll()
        {
            return _notesService.FindAll();
        }
        public void SetFlag(Notes notes)
        {
            _notesService.SetFlag(notes);
        }
    }
}
