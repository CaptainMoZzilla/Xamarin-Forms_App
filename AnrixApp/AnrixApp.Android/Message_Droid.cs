using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AnrixApp.Droid;
using AnrixApp.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(Message_Droid))]
namespace AnrixApp.Droid
{
    public class Message_Droid : IMessage
    {
        public void LongTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortTime(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}