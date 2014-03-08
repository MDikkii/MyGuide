﻿using Caliburn.Micro;
using MyGuide.Resources;
using MyGuide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyGuide.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IMessageDialogService _msgDialogServ;

        public MainPageViewModel()
        {
            if (Execute.InDesignMode)
                LoadDesignData();
        }

        public MainPageViewModel(
            INavigationService navigationService,
            IMessageDialogService msgDialogService)
            : base(navigationService)
        {
            // Uncomment to use design time data as test data
            // LoadDesignData();

            _msgDialogServ = msgDialogService;
        }

        #region Properties

        private string _welcomeText;

        public string WelcomeText
        {
            get { return _welcomeText; }
            set { _welcomeText = value; NotifyOfPropertyChange(() => WelcomeText); }
        }

        #endregion Properties

        #region Commands

        public void ShowAboutZoo()
        {
            _navigation.UriFor<AboutZooPageViewModel>().Navigate();
        }

        public void ShowOptions()
        {
            _navigation.UriFor<OptionsPageViewModel>().Navigate();
        }

        public void ShowSightsee()
        {
            _navigation.UriFor<SightseeingPageViewModel>().Navigate();
        }

        public void ShowTravelDirections()
        {
        }

        #endregion Commands

        public async void OnClose(CancelEventArgs args)
        {
            args.Cancel = true;
            bool exit = await _msgDialogServ.ShowDialog(AppResources.ExitDlgTitle,
                AppResources.ExitDlgMessage, DialogType.YesNo);
            if (exit)
            {
                App.Current.Terminate();
            }
        }

        private void LoadDesignData()
        {
            WelcomeText = DesignData.LoremImpusGenerator.Generate(5);
        }
    }
}