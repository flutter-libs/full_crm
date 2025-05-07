import 'package:flutter/material.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/services/campaign_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:intl/intl.dart';
import 'package:frontend/constants/form_fields.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;
import 'package:jwt_decode/jwt_decode.dart';
import 'package:shared_preferences/shared_preferences.dart';


class CampaignCreateScreen extends StatefulWidget {
  const CampaignCreateScreen({super.key});

  static const String id = "campaign_create_screen";

  @override
  State<CampaignCreateScreen> createState() => _CampaignCreateScreenState();
}

class _CampaignCreateScreenState extends State<CampaignCreateScreen> {
  final _formKey = GlobalKey<FormState>();
  final CampaignApiService _apiService = CampaignApiService();

  final _nameController = TextEditingController();
  final _descriptionController = TextEditingController();
  final _typeController = TextEditingController();
  final _statusController = TextEditingController();
  final _budgetController = TextEditingController();
  final _actualCostController = TextEditingController();
  final _expectedResponsesController = TextEditingController();
  final _actualResponsesController = TextEditingController();
  final _expectedSalesController = TextEditingController();
  final _actualSalesController = TextEditingController();

  DateTime? _startDate;
  DateTime? _endDate;

  Future<void> _pickDate(BuildContext context, bool isStartDate) async {
    final picked = await showDatePicker(
      context: context,
      initialDate: isStartDate ? (_startDate ?? DateTime.now()) : (_endDate ??
          DateTime.now()),
      firstDate: DateTime(2000),
      lastDate: DateTime(2100),
    );
    if (picked != null) {
      setState(() {
        if (isStartDate) {
          _startDate = picked;
        } else {
          _endDate = picked;
        }
      });
    }
  }

  void _onSubmit() async {
    if (_formKey.currentState!.validate()) {
      _formKey.currentState!.save();

      Campaign campaign = Campaign(
        name: _nameController.text,
        description: _descriptionController.text,
        type: _typeController.text,
        status: _statusController.text,
        startDate: _startDate,
        endDate: _endDate,
        budget: double.tryParse(_budgetController.text),
        actualCost: double.tryParse(_actualCostController.text),
        expectedResponses: int.tryParse(_expectedResponsesController.text),
        actualResponses: int.tryParse(_actualResponsesController.text),
        expectedSales: double.tryParse(_expectedSalesController.text),
        actualSales: double.tryParse(_actualSalesController.text),
        dateCreated: DateTime.now(),
        dateUpdated: DateTime.now(),
        createdByUserId: _apiService.getCurrentUser.toString(),
      );

      try {
        bool success = await _apiService.createCampaign(campaign);
        if (success) {
          alert.showSuccessToast(context, 'Campaign Created Successfully', 'Campaign Created');
          Navigator.pop(context);
        } else {
          alert.showErrorToast(context, 'Campaign Creation Failed. Please try again later.', 'Creation Failed');
        }
      } catch (e) {
        alert.showErrorToast(context, 'Unexpected error: $e', 'Error');
      }
    }
  }


  @override
  void dispose() {
    _nameController.dispose();
    _descriptionController.dispose();
    _typeController.dispose();
    _statusController.dispose();
    _budgetController.dispose();
    _actualCostController.dispose();
    _expectedResponsesController.dispose();
    _actualResponsesController.dispose();
    _expectedSalesController.dispose();
    _actualSalesController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideNavDrawer(),
        appBar: CustomAppBar(title: 'Create Campaign', showBackButton: false),
        body: Padding(
            padding: const EdgeInsets.all(16.0),
            child: Form(
                key: _formKey,
                child: ListView(
                    children: [
                      TextFormField(
                        controller: _nameController,
                        decoration: showFormFieldStyle('Name'),
                        validator: (value) =>
                        value!.isEmpty
                            ? 'Name is required'
                            : null,
                      ),
                      TextFormField(
                        controller: _descriptionController,
                        minLines: 5,
                        maxLines: 25,
                        decoration: showFormFieldStyle('Description')
                      ),
                      TextFormField(
                        controller: _typeController,
                        decoration: showFormFieldStyle('Type'),
                      ),
                      TextFormField(
                        controller: _statusController,
                        decoration: showFormFieldStyle('Status')
                      ),
                      Row(
                        children: [
                          Expanded(
                            child: Text(
                                'Start Date: ${_startDate != null ? DateFormat
                                    .yMd().format(_startDate!) : 'Not set'}'),
                          ),
                          TextButton(
                            onPressed: () => _pickDate(context, true),
                            child: const Text('Pick Start'),
                          ),
                        ],
                      ),
                      Row(
                          children: [
                            Expanded(
                              child: Text(
                                  'End Date: ${_endDate != null ? DateFormat
                                      .yMd().format(_endDate!) : 'Not set'}'),
                            ),
                            TextButton(
                                onPressed: () => _pickDate(context, false),
                                child: const Text('Pick End')),
                          ]
                      ),
                      Row(
                        children: [
                          TextFormField(
                            controller: _budgetController,
                            decoration: showFormFieldStyle('Budget'),
                            keyboardType: TextInputType.number,
                            validator: (value) => value!.isEmpty ? 'Budget is required' : null,
                          ),
                          TextFormField(
                            controller: _actualCostController,
                            decoration: showFormFieldStyle('Actual Costs'),
                            keyboardType: TextInputType.number,
                            validator: (value) => value!.isEmpty ? 'Actual Costs is required' : null,
                          ),
                          TextFormField(
                            controller: _expectedSalesController,
                            decoration: showFormFieldStyle('Expected Sales'),
                            keyboardType: TextInputType.number,
                            validator: (value) => value!.isEmpty ? 'Expected Sales is required' : null,
                          ),
                          TextFormField(
                            controller: _actualSalesController,
                            decoration: showFormFieldStyle('Actual Sales'),
                            keyboardType: TextInputType.number,
                            validator: (value) => value!.isEmpty ? 'Actual Sales is required' : null,
                          ),
                        ],
                      ),
                      Row(
                        children: [
                          TextFormField(
                            controller: _expectedResponsesController,
                            decoration: showFormFieldStyle('Expected Responses'),
                            keyboardType: TextInputType.number,
                            validator: (value) => value!.isEmpty ? 'Expected Responses is required' : null,
                          ),
                          TextFormField(
                            controller: _actualResponsesController,
                            decoration: showFormFieldStyle('Actual Responses'),
                            keyboardType: TextInputType.number,
                            validator: (value) => value!.isEmpty ? 'Actual Responses is required' : null,
                          ),
                        ],
                      ),
                      ElevatedButton(
                        onPressed: _onSubmit,
                        child: SizedBox(
                          width: double.infinity,
                          height: 60.0,
                          child: Text(
                              'Create Campaign',
                            style: TextStyle(
                              color: Colors.white,
                              fontFamily: 'Ubuntu',
                              fontWeight: FontWeight.w700,
                              fontSize: 18.0
                            ),
                          ),
                        ),
                      ),
                    ]
                )
            )
        )
    );
  }
}
