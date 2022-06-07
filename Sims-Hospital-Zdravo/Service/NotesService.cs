using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Service
{
    public class NotesService
    {
        private NotesRepository _notesRepository;
        public NotesService() 
        {
            _notesRepository = new NotesRepository();
        }
        public void Create(Notes notes)
        {
            _notesRepository.Create(notes);
        }
        public List<Notes> FindAll()
        {
            return _notesRepository.FindAll();
        }
        public void SetFlag(Notes notes)
        {
            _notesRepository.SetFlag(notes);
        }
    }
}
