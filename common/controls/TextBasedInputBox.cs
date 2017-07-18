using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace boss.client.win.common.controls
{
    public abstract class TextBasedInputBox : TextBox, IInputBox
    {
        private readonly string name;
        private bool keyboardForcusChangedBetweenLeftButtonDownAndUp;

        protected TextBasedInputBox(Parameter parameter)
        {
            name = parameter.name;
            Initialized += TextBasedInputBox_Initialized; ;
        }

        public string GetName()
        {
            return name ?? Name;
        }

        public object GetValue()
        {
            return Value;
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(object), typeof(TextBasedInputBox), new PropertyMetadata(default(object)));

        public virtual object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(value); }
        }

        public static readonly DependencyProperty FullValueProperty = DependencyProperty.Register(
            "FullValue", typeof(object), typeof(TextBasedInputBox), new PropertyMetadata(default(object)));

        public object FullValue
        {
            get { return (object) GetValue(FullValueProperty); }
            set { SetFullValue(value); }
        }

        public void ClearValue()
        {
            SetFullValue(null);
        }

        public void ChangeValue(object value)
        {
            SetValue(value);
        }

        private void SetValue(object value)
        {
            SetFullValue(ResolveFromValue(value));
        }

        public void ChangeFullValue(object fullValue)
        {
            SetFullValue(fullValue);
        }

        private void SetFullValue(object fullValue)
        {
            var value = ExtractValue(fullValue);
            SetValue(FullValueProperty, fullValue);
            SetValue(ValueProperty, value);
            UpdateViewText();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            SetFullValue(ParseFullValue());
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            UpdateViewText();
            if (!IsLeftMouseButtonDown)
            {
                SelectAll();
            }
            else
            {
                SelectionLength = 0;
                keyboardForcusChangedBetweenLeftButtonDownAndUp = true;
            }
        }
        
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            IsLeftMouseButtonDown = true;
            keyboardForcusChangedBetweenLeftButtonDownAndUp = false;
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            IsLeftMouseButtonDown = false;
            if (!keyboardForcusChangedBetweenLeftButtonDownAndUp) return;
            if (SelectionLength < 1)
            {
                SelectAll();
            }
        }

        private void TextBasedInputBox_Initialized(object sender, EventArgs e)
        {
            UpdateViewText();
        }

        private void UpdateViewText()
        {
            Text = IsKeyboardFocused ? ToInputText() : ToDisplayText();
        }

        protected bool IsLeftMouseButtonDown
        {
            get; private set;
        }

        protected virtual object ResolveFromValue(object value)
        {
            return value;
        }

        protected virtual object ExtractValue(object fullValue)
        {
            return fullValue;
        }

        protected abstract object ParseFullValue();

        protected abstract string ToInputText();

        protected abstract string ToDisplayText();
    }
}