﻿using ProcMonX.ViewModels;
using Syncfusion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zodiacon.WPF;

namespace ProcMonX.ViewModels.Tabs {
    [TabItem(Text = "All Events", Icon = "/icons/tabs/event.ico")]
    sealed class EventsViewModel : TabItemViewModelBase {
        public object Parent { get; }

        public EventsViewModel(object parent, IList<TraceEventDataViewModel> events) {
            Parent = parent;
            Events = events;
       }

        public bool IsNotMonitoring { get; set; }

        internal override bool CanClose => false;

        public IList<TraceEventDataViewModel> Events { get; }

        public ICollectionViewAdv View { get; set; }
        public string FilterText {
            get => _filterText;
            set {
                if (SetProperty(ref _filterText, value)) {
                    if (string.IsNullOrWhiteSpace(value))
                        View.Filter = null;
                    else {
                        var text = value.ToLower();
                        View.Filter = obj => {
                            var item = (TraceEventDataViewModel)obj;
                            return item.ProcessName.ToLower().Contains(text) || item.TypeAsString.ToLower().Contains(text)
                                || item.Opcode.ToLower().Contains(text) || item.Details.ToLower().Contains(text);
                        };
                    }
                    View.RefreshFilter();
                }
            }
        }
        string _filterText;
    }
}
