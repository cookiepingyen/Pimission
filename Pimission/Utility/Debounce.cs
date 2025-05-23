using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pimission.Utility
{
    public static class Debounce
    {
        public static Timer timer;
        public static Window window;

        public static void DelayCallback(this Window window, Action<object> action, object data, int debounceTime = 0)
        {
            if (debounceTime != 0 && timer != null)
            {
                timer.Change(debounceTime, Timeout.Infinite);
            }
            else
            {

                Debounce.window = window;
                window.Tag = action;
                timer = new Timer(Callback2, data, debounceTime, Timeout.Infinite);
            }
        }
        public static void DelayCallback(this Window form, Action action, int debounceTime = 0)
        {
            if (debounceTime != 0 && timer != null)
            {
                timer.Change(debounceTime, Timeout.Infinite);
            }
            else
            {
                Debounce.window = form;
                form.Tag = action;
                timer = new Timer(Callback, null, debounceTime, Timeout.Infinite);
            }
        }

        public static void Callback(object data)
        {
            window.Dispatcher.Invoke(new Action(() =>
            {
                Action action = (Action)window.Tag;
                action.Invoke();
            }));
        }

        public static void Callback2(object data)
        {
            window.Dispatcher.Invoke(new Action(() =>
            {
                Action<object> action = (Action<object>)window.Tag;
                action.Invoke(data);
            }));
        }

    }
}
