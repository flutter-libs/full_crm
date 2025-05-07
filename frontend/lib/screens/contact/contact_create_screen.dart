import 'package:flutter/material.dart';
import 'package:frontend/constants/form_fields.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/screens/contact/contact_list_screen.dart';
import 'package:frontend/services/contact_api_service.dart';
import 'package:frontend/widgets/country_selector.dart';
import 'package:frontend/widgets/image_upload.dart';
import 'package:frontend/widgets/us_state_selector.dart';
import 'package:intl/intl.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class ContactCreateScreen extends StatefulWidget {
  const ContactCreateScreen({super.key});
  static const String id = "contact_create_screen";
  @override
  State<ContactCreateScreen> createState() => _ContactCreateScreenState();
}

class _ContactCreateScreenState extends State<ContactCreateScreen> {
  final _formKey = GlobalKey<FormState>();
  final ContactApiService _apiService = ContactApiService();
  String? _imageBase64;
  // Form fields
  final TextEditingController _firstNameController = TextEditingController();
  final TextEditingController _lastNameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _phoneNumberController = TextEditingController();
  final TextEditingController _companyNameController = TextEditingController();
  final TextEditingController _jobTitleController = TextEditingController();
  final TextEditingController _cityController = TextEditingController();
  final TextEditingController _addressLine1Controller = TextEditingController();
  final TextEditingController _addressLine2Controller = TextEditingController();
  final TextEditingController _stateController = TextEditingController();
  final TextEditingController _zipCodeController = TextEditingController();
  final TextEditingController _countryController = TextEditingController();
  final TextEditingController _imageUrlController = TextEditingController();

  bool _isSubmitting = false;

  Future<void> _onSubmit() async {
    if (_formKey.currentState!.validate()) {
      setState(() => _isSubmitting = true);

      final contact = Contact(
        firstName: _firstNameController.text,
        lastName: _lastNameController.text,
        email: _emailController.text,
        phoneNumber: _phoneNumberController.text,
        companyName: _companyNameController.text,
        jobTitle: _jobTitleController.text,
        dateCreated: DateTime.now(),
        addressLine1: _addressLine1Controller.text,
        addressLine2: _addressLine2Controller.text,
        city: _cityController.text,
        state: _stateController.text,
        zipCode: _zipCodeController.text,
        country: _countryController.text,
        imageUrl: _imageUrlController.text

      );


      setState(() => _isSubmitting = false);

      try {
        bool success = await _apiService.createContact(contact);
        if (success) {
          alert.showSuccessToast(context, 'Contact Created Successfully', 'Contact Created');
          Navigator.pushNamed(context, ContactListScreen.id);
        } else {
          alert.showErrorToast(context, 'Contact Creation Failed. Please try again later.', 'Creation Failed');
        }
      } catch (e) {
        alert.showErrorToast(context, 'Unexpected error: $e', 'Error');
      }
    }
  }

  @override
  void dispose() {
    _firstNameController.dispose();
    _lastNameController.dispose();
    _emailController.dispose();
    _phoneNumberController.dispose();
    _companyNameController.dispose();
    _jobTitleController.dispose();
    _addressLine1Controller.dispose();
    _addressLine2Controller.dispose();
    _cityController.dispose();
    _stateController.dispose();
    _zipCodeController.dispose();
    _countryController.dispose();
    _imageUrlController.dispose();
    super.dispose();
  }
  void _onImageSelected(String base64Image) {
    setState(() {
      _imageBase64 = base64Image;
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(title: Text('Create Contact')),
        body: SingleChildScrollView(
            padding: EdgeInsets.all(16),
            child: Card(
                elevation: 4,
                shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12)),
                child: Padding(
                    padding: const EdgeInsets.all(16),
                    child: Form(
                      key: _formKey,
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Column(
                          children: [
                            const SizedBox(height: 30.0),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Row(
                                children: [
                                  TextFormField(
                                    controller: _firstNameController,
                                    decoration: showFormFieldStyle('First Name'),
                                  ),
                                  TextFormField(
                                    controller: _lastNameController,
                                    decoration: showFormFieldStyle('Last Name'),
                                  ),
                                ],
                              ),
                            ),
                              Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Row(
                                  children: [
                                    const SizedBox(height: 15.0),
                                    TextFormField(
                                      controller: _jobTitleController,
                                      decoration: showFormFieldStyle('Job Title'),
                                    ),
                                    TextFormField(
                                      controller: _companyNameController,
                                      decoration: showFormFieldStyle('Company Name'),
                                    ),
                                  ],
                                ),
                              ),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Row(
                                children: [
                                  const SizedBox(height: 15.0),
                                  TextFormField(
                                    controller: _emailController,
                                    decoration: showFormFieldStyle('Email'),
                                  ),
                                  TextFormField(
                                    controller: _phoneNumberController,
                                    decoration: showFormFieldStyle('Phone Number'),
                                  ),
                                ],
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Row(
                                children: [
                                  const SizedBox(height: 15.0),
                                  TextFormField(
                                    controller: _addressLine1Controller,
                                    decoration: showFormFieldStyle('Address Line 1'),
                                  ),
                                  TextFormField(
                                    controller: _addressLine2Controller,
                                    decoration: showFormFieldStyle('Address Line 2'),
                                  ),
                                  TextFormField(
                                    controller: _cityController,
                                    decoration: showFormFieldStyle('City'),
                                  ),
                                  TextFormField(
                                    controller: _zipCodeController,
                                    decoration: showFormFieldStyle('Zip Code'),
                                  ),
                                ],
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Row(
                                children: [
                                  USStateSelector(),
                                  CountrySelector(),
                                ],
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Row(
                                children: [
                                  const SizedBox(height: 15.0),
                                  ImageUploadWidget(onImageSelected: _onImageSelected)
                                ],
                              ),
                            ),
                            const SizedBox(height: 15.0),
                            Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: ElevatedButton(onPressed: () => _onSubmit,
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: Colors.indigo,
                                  ),
                                  child: const SizedBox(width: double.infinity, height: 50.0, child: Text('Add Contact',
                                    style: TextStyle(color: Colors.white, fontFamily: 'Ubuntu', fontWeight: FontWeight.w700),),)),
                            )
                          ],
                        ),
                      ),
                    )
                )
            )
        )
    );
  }
}