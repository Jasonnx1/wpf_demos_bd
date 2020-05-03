using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Documents;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ContactModel selectedContact;
        private List<ContactModel> contacts;

        public DataSet ds { get; set; }


        public List<ContactModel> Contacts
        {
            get => contacts;
            set {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; set; }
        public RelayCommand CreateContactCommand { get; set; }
        public RelayCommand UpdateContactCommand { get; set; }

        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);
            CreateContactCommand = new RelayCommand(CreateContact);
            UpdateContactCommand = new RelayCommand(UpdateContact);

            SelectedContact = PhoneBookBusiness.GetContactByID(1);
            Contacts = PhoneBookBusiness.GetContacts();
          
        }

        private void CreateContact(object parameter)
        {
            SelectedContact = new ContactModel();
            SelectedContact.ContactID = 0;
        }

        private void UpdateContact(object parameter)
        {
            PhoneBookBusiness.UpdateContactModel(SelectedContact);   
        }
            


        private void DeleteContact(object parameter)
        {

            PhoneBookBusiness.DeleteContact(SelectedContact.ContactID);
              
        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            } else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    Contacts = PhoneBookBusiness.GetContactsById(output);

                    try
                    {

                        SelectedContact = Contacts[0];

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Aucun Contact Trouver!", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;

                case "name":
                    Contacts = PhoneBookBusiness.GetContactsByName(input);

                    try
                    {

                        SelectedContact = Contacts[0];

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Aucun Contact Trouver!", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                    

                    
                    

                    
                   
                    
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }


        }
    }
}
