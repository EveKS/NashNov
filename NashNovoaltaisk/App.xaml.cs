﻿using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using NashNovoaltaisk.Services;
using NashNovoaltaisk.ViewModels;
using NashNovoaltaisk.Views;

namespace NashNovoaltaisk
{
    /// <summary>
    ///     Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    internal sealed partial class App
    {
        private WinRTContainer _container;

        protected override void Configure()
        {
            _container = new WinRTContainer();

            _container.RegisterWinRTServices();
            _container.PerRequest<INewsService, NewsService>();
            _container.PerRequest<MainViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;
            DisplayRootView<MainView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}