using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Xml;

namespace Colourblind.Core
{
    public class Email
    {
        #region Fields
        
        private string _subject;
        private string _from;
        private string _body;

        #endregion

        #region Properties

        public EmailTemplate Template
        {
            get;
            private set;
        }

        public string Subject
        {
            get
            {
                string result = _subject;
                if (String.IsNullOrEmpty(result) && Template != null)
                    result = Template.Subject;
                return result;
            }
            set
            {
                _subject = value;
            }
        }

        public string From
        {
            get
            {
                string result = _from;
                if (String.IsNullOrEmpty(result) && Template != null)
                    result = Template.From;
                return result;
            }
            set
            {
                _from = value;
            }
        }

        public string Body
        {
            get
            {
                string result = _body;
                if (String.IsNullOrEmpty(result) && Template != null)
                    result = Template.Body;
                return result;
            }
            set
            {
                _body = value;
            }
        }

        public IList<string> Recipients
        {
            get;
            set;
        }

        public IList<string> Cc
        {
            get;
            set;
        }

        public IList<string> Bcc
        {
            get;
            set;
        }

        public IList<string> Attachments
        {
            get;
            set;
        }

        public bool IsHtml
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public Email()
        {
            Recipients = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
            IsHtml = true;
        }

        internal Email(EmailTemplate template) : this()
        {
            Template = template;
        }

        #endregion

        #region Methods

        public void Send()
        {
            MailMessage message = new MailMessage();

            foreach (string recipient in Recipients)
                message.To.Add(new MailAddress(recipient));

            foreach (string cc in Cc)
                message.CC.Add(new MailAddress(cc));

            foreach (string bcc in Bcc)
                message.Bcc.Add(new MailAddress(bcc));

            message.From = new MailAddress(From);
            message.IsBodyHtml = IsHtml;
            message.Subject = Subject;
            message.Body = Body;

            foreach (string attachmentFilename in Attachments)
            {
                Attachment a = new Attachment(attachmentFilename);
                message.Attachments.Add(a);
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
        }

        #endregion
    }

    public class EmailTemplate
    {
        #region Fields

        private XmlDocument _xmlDocument;
        private Dictionary<string, string> _parameters;
        private List<string> _keys;

        #endregion

        #region Properties

        internal string From
        {
            get
            {
                string result = GetTemplateParameter("from");
                foreach (KeyValuePair<string, string> data in _parameters)
                    result = result.Replace("%" + data.Key + "%", data.Value);
                return result;
            }
        }

        internal string Subject
        {
            get
            {
                string result = GetTemplateParameter("subject");
                foreach (KeyValuePair<string, string> data in _parameters)
                    result = result.Replace("%" + data.Key + "%", data.Value);
                return result;
            }
        }

        internal string Body
        {
            get
            {
                string result = GetTemplateParameter("emailBody");
                foreach (KeyValuePair<string, string> data in _parameters)
                    result = result.Replace("%" + data.Key + "%", data.Value);
                return result;
            }
        }

        #endregion

        #region Constructors

        public EmailTemplate(string filename)
        {
            _parameters = new Dictionary<string, string>();
            _keys = new List<string>();

            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(filename);

            LoadKeys();
        }

        #endregion

        #region Methods

        public void AddParameter(string key, object value)
        {
            if (_keys.Contains(key))
                _parameters.Add(key , value.ToString());
            else
                throw new IndexOutOfRangeException("Key \"" + key + "\"not found in email template");
        }

        public Email GetEmail()
        {
            Email email = new Email();

            email.From = From;
            email.Subject = Subject;
            email.Body = Body;

            ValidateParameters();

            // Add parameters
            foreach (string key in _keys)
            {
                email.Subject.Replace("%" + key + "%", _parameters[key]);
                email.Body.Replace("%" + key + "%", _parameters[key]);
            }

            return email;
        }

        private void ValidateParameters()
        {
            foreach (string key in _keys)
            {
                if (!_parameters.ContainsKey(key))
                    throw new IndexOutOfRangeException("Missing parameter for key \"" + key + "\"");
            }
        }

        private void LoadKeys()
        {
            foreach (XmlNode keyNode in _xmlDocument.DocumentElement.SelectNodes("key"))
                _keys.Add(keyNode.InnerText);
        }

        private string GetTemplateParameter(string name)
        {
            string result = null;
            XmlNode node = _xmlDocument.DocumentElement.SelectSingleNode(name);
            if (node != null)
                result = node.InnerText;
            return result;
        }

        public static Email Load(string filename)
        {
            EmailTemplate template = new EmailTemplate(ConfigurationManager.AppSettings["EmailTemplatePath"] + filename + ".xml");
            return new Email(template);
        }

        public static Email LoadFromFullPath(string filename)
        {
            EmailTemplate template = new EmailTemplate(filename + ".xml");
            return new Email(template);
        }

        public static Email LoadFromAppRelativePath(string filename)
        {
            EmailTemplate template = new EmailTemplate(Environment.CurrentDirectory + "\\" + filename + ".xml");
            return new Email(template);
        }

        #endregion
    }
}