using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class NotesRepository : INotesRepository
    {
        private NotesDataHandler _notesDataHandler;
        private List<Notes> _notes;
        public NotesRepository()
        {
           _notesDataHandler = new NotesDataHandler();
        }
        public void Create(Notes notes)
        {
            LoadDataFromFile();
            _notes.Add(notes);
            LoadDataToFile();
        }
        public List<Notes> FindAll()
        {
            LoadDataFromFile();
            return _notes;
        }
        public void LoadDataFromFile()
        {
            _notes = _notesDataHandler.ReadAll();
        }
        public void LoadDataToFile() 
        {
            _notesDataHandler.Write(_notes);
        }

        public void SetFlag(Notes notes)
        {
            LoadDataFromFile();
            foreach (Notes note in _notes)
            {
                if (note.Reminder.Equals(notes.Reminder) && note.Text == notes.Text)
                {
                    note.Flag = false;
                    LoadDataToFile();
                    return;
                }
            }
        }
    }
}
