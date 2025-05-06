import 'package:flutter/material.dart';
import 'package:dio/dio.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/screens/lead/lead_list_screen.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class LeadUpdateScreen extends StatefulWidget {
  final int? leadId;
  static const String id = "lead_update_screen";
  const LeadUpdateScreen({super.key, this.leadId});

  @override
  State<LeadUpdateScreen> createState() => _LeadUpdateScreenState();
}

class _LeadUpdateScreenState extends State<LeadUpdateScreen> {
  final _formKey = GlobalKey<FormState>();
  late Lead _lead;
  late TextEditingController _nameController;
  late TextEditingController _emailController;
  late TextEditingController _phoneNumberController;
  late TextEditingController _faxController;
  late TextEditingController _addressController;
  late TextEditingController _cityController;
  late TextEditingController _stateController;
  late TextEditingController _zipController;
  late TextEditingController _countryController;
  late TextEditingController _websiteController;
  late TextEditingController _notesController;
  @override
  void initState() {
    super.initState();
    _lead = Lead(
      id: widget.leadId,
      leadName: '',
      leadEmail: '',
      leadPhone: '',
      leadFax: '',
      leadAddress: '',
      leadCity: '',
      leadState: '',
      leadZip: '',
      leadCountry: '',
      leadNotes: '',
      leadWebsite: '',
      updated: DateTime.now(),
    );
    _nameController = TextEditingController(text: _lead.leadName);
    _emailController = TextEditingController(text: _lead.leadEmail);
    _phoneNumberController = TextEditingController(text: _lead.leadPhone);
    _faxController = TextEditingController(text: _lead.leadFax);
    _addressController = TextEditingController(text: _lead.leadAddress);
    _cityController = TextEditingController(text: _lead.leadCity);
    _stateController = TextEditingController(text: _lead.leadState);
    _zipController = TextEditingController(text: _lead.leadZip);
    _countryController = TextEditingController(text: _lead.leadCountry);
    _notesController = TextEditingController(text: _lead.leadNotes);
    _websiteController = TextEditingController(text: _lead.leadWebsite);
    _fetchLeadData();
  }

  // Fetch the lead data from the backend
  Future<void> _fetchLeadData() async {
    try {
      var response = await Dio().get('https://localhost:5244/api/Leads/${widget.leadId}');
      setState(() {
        _lead = Lead.fromJson(response.data);
        _nameController.text = _lead.leadName!;
        _emailController.text = _lead.leadEmail!;
        _phoneNumberController.text = _lead.leadPhone!;
        _faxController.text = _lead.leadFax!;
        _addressController.text = _lead.leadAddress!;
        _cityController.text = _lead.leadCity!;
        _stateController.text = _lead.leadState!;
        _zipController.text = _lead.leadZip!;
        _countryController.text = _lead.leadCountry!;
        _notesController.text = _lead.leadNotes!;
        _websiteController.text = _lead.leadWebsite!;
      });
    } catch (e) {
      alert.showErrorToast(context, 'Fetching error for lead: $e', 'Fetching Error');
    }
  }

  // Update the lead data
  Future<void> _updateLead() async {
    if (_formKey.currentState!.validate()) {
      _formKey.currentState!.save();
      try {
        // Update lead on the server
        await Dio().put(
          'http://localhost:5244/api/Lead/${_lead.id}',
          data: _lead.toJson(),
        );
        alert.showSuccessToast(context, 'Lead updated successfully!', 'Lead Updated');
        Navigator.pushNamed(context, LeadListScreen.id);
      } catch (e) {
        alert.showErrorToast(context, 'Error updating lead', 'Updating Error');
        Navigator.of(context).pop();
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'Update Lead: ${_lead.leadName}', showBackButton: false),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Center(
          child: Card(
          child: Form(
            key: _formKey,
            child: SingleChildScrollView(
              child: Column(
                children: [
                  TextFormField(
                    controller: _nameController,
                    decoration: InputDecoration(labelText: 'Name'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the name';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadName = value!;
                    },
                  ),
                  TextFormField(
                    controller: _emailController,
                    decoration: InputDecoration(labelText: 'Email'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the email';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadEmail = value!;
                    },
                  ),
                  TextFormField(
                    controller: _phoneNumberController,
                    decoration: InputDecoration(labelText: 'Phone Number'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the phone number';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadPhone = value!;
                    },
                  ),
                  TextFormField(
                    controller: _faxController,
                    decoration: InputDecoration(labelText: 'Fax'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the status';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadFax = value!;
                    },
                  ),
                  Row(
                    children: [
                      TextFormField(
                        controller: _addressController,
                        decoration: InputDecoration(labelText: 'Address'),
                        validator: (value) {
                          if (value!.isEmpty) {
                            return 'Please enter the address';
                          }
                          return null;
                        },
                        onSaved: (value) {
                          _lead.leadAddress = value!;
                        },
                      ),
                      TextFormField(
                        controller: _cityController,
                        decoration: InputDecoration(labelText: 'City'),
                        validator: (value) {
                          if (value!.isEmpty) {
                            return 'Please enter the city';
                          }
                          return null;
                        },
                        onSaved: (value) {
                          _lead.leadCity = value!;
                        },
                      ),
                    ],
                  ),
                  Row(
                    children: [
                      TextFormField(
                        controller: _stateController,
                        decoration: InputDecoration(labelText: 'State'),
                        validator: (value) {
                          if (value!.isEmpty) {
                            return 'Please enter the state';
                          }
                          return null;
                        },
                        onSaved: (value) {
                          _lead.leadState = value!;
                        },
                      ),
                      TextFormField(
                        controller: _zipController,
                        decoration: InputDecoration(labelText: 'Zip'),
                        validator: (value) {
                          if (value!.isEmpty) {
                            return 'Please enter the zip';
                          }
                          return null;
                        },
                        onSaved: (value) {
                          _lead.leadZip = value!;
                        },
                      ),
                    ],
                  ),
                  TextFormField(
                    controller: _countryController,
                    decoration: InputDecoration(labelText: 'Country'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the country';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadCountry = value!;
                    },
                  ),
                  TextFormField(
                    controller: _websiteController,
                    decoration: InputDecoration(labelText: 'Website'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the website';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadWebsite = value!;
                    },
                  ),
                  TextFormField(
                    controller: _notesController,
                    decoration: InputDecoration(labelText: 'Notes'),
                    validator: (value) {
                      if (value!.isEmpty) {
                        return 'Please enter the notes';
                      }
                      return null;
                    },
                    onSaved: (value) {
                      _lead.leadAddress = value!;
                    },
                  ),
                  SizedBox(height: 20),
                  ElevatedButton(
                    onPressed: _updateLead,
                    child: Text('Update Lead'),
                  ),
                ],
              ),
            ),
          ),
        ),
        ),
      ),
    );
  }
}