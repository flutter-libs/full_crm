import 'package:flutter/material.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/services/lead_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';

class LeadCreateScreen extends StatefulWidget {
  static const String id = "lead_create_screen";

  const LeadCreateScreen({super.key});
  @override
  State<LeadCreateScreen> createState() => _LeadCreateScreenState();
}

class _LeadCreateScreenState extends State<LeadCreateScreen> {
  final _formKey = GlobalKey<FormState>();
  final _lead = Lead();
  final LeadApiService _leadService = LeadApiService();

  bool _isSubmitting = false;

  void _submitForm() async {
    if (_formKey.currentState!.validate()) {
      _formKey.currentState!.save();

      setState(() {
        _isSubmitting = true;
      });

      _lead.created = DateTime.now();
      _lead.updated = DateTime.now();

      try {
        final success = await _leadService.createLead(_lead);
        if (success != null) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text('Lead created successfully')),
          );
          Navigator.pop(context);
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text('Failed to create lead')),
          );
        }
      } catch (e) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Error: ${e.toString()}')),
        );
      } finally {
        setState(() {
          _isSubmitting = false;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Create Lead'),
      drawer: SideNavDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: _isSubmitting
            ? Center(child: CircularProgressIndicator())
            : Form(
          key: _formKey,
          child: ListView(
            children: [
              _buildTextField('Name', 'Enter lead name', (value) => _lead.leadName = value),
              _buildTextField('Email', 'Enter email', (value) => _lead.leadEmail = value),
              _buildTextField('Phone', 'Enter phone', (value) => _lead.leadPhone = value),
              _buildTextField('Address', 'Enter address', (value) => _lead.leadAddress = value),
              _buildTextField('City', 'Enter city', (value) => _lead.leadCity = value),
              _buildTextField('State', 'Enter state', (value) => _lead.leadState = value),
              _buildTextField('Zip', 'Enter zip', (value) => _lead.leadZip = value),
              _buildTextField('Country', 'Enter country', (value) => _lead.leadCountry = value),
              _buildTextField('Fax', 'Enter fax', (value) => _lead.leadFax = value),
              _buildTextField('Website', 'Enter website', (value) => _lead.leadWebsite = value),
              _buildTextField('Notes', 'Enter notes', (value) => _lead.leadNotes = value, maxLines: 3),
              SizedBox(height: 20),
              ElevatedButton(
                onPressed: _submitForm,
                child: Text('Create Lead'),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildTextField(String label, String hint, Function(String?) onSaved, {int maxLines = 1}) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8),
      child: TextFormField(
        decoration: InputDecoration(labelText: label, hintText: hint, border: OutlineInputBorder()),
        validator: (value) {
          if (value == null || value.isEmpty) {
            return '$label is required';
          }
          return null;
        },
        onSaved: onSaved,
        maxLines: maxLines,
      ),
    );
  }
}