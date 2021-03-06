﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace SFHackDay.Model
{
    class MMainControl:BindableBase
    {
        string _enteredText;

        public string EnteredText
        {
            get { return _enteredText; }
            set { _enteredText = value; OnPropertyChanged(() => this.EnteredText); }
        }
        string _timeToCrack;

        public string TimeToCrack
        {
            get { return _timeToCrack; }
            set { _timeToCrack = value; OnPropertyChanged(()=> this.TimeToCrack); }
        }

        string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(()=> this.Message); }
        }
        int _strengthNumber;

        public int StrengthNumber
        {
            get { return _strengthNumber; }
            set { _strengthNumber = value; OnPropertyChanged(() => this.StrengthNumber); }
        }

        string _color;

        public string Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(() => this.Color); }
        }

    }
}
