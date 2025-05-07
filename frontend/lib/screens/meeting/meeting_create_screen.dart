import 'package:flutter/material.dart';
import 'package:frontend/services/meeting_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class MeetingCreateScreen extends StatefulWidget {
  const MeetingCreateScreen({super.key});
  static const String id = "meeting_create_screen";
  @override
  State<MeetingCreateScreen> createState() => _MeetingCreateScreenState();
}

class _MeetingCreateScreenState extends State<MeetingCreateScreen> {
  final _formKey = GlobalKey<FormState>();
  final MeetingAPIService _meetingService = MeetingAPIService();
  final TextEditingController _titleController = TextEditingController();
  final TextEditingController _descriptionController = TextEditingController();
  String? _userId;

  bool _isSubmitting = false;

  Future<void> _submit() async {
    if (!_formKey.currentState!.validate()) return;

    setState(() => _isSubmitting = true);

    try {
      final Map<String, dynamic> meetingData = {
        "userId": _userId ?? "default-user-id",
        "meeting": {
          "title": _titleController.text,
          "description": _descriptionController.text,
          "dateCreated": DateTime.now().toIso8601String(),
        }
      };

      final newUserMeeting = await _meetingService.createMeeting(meetingData);
      alert.showSuccessToast(context, 'Meeting created successfully', 'Meeting Created');
      Navigator.pop(context, newUserMeeting);
    } catch (e) {
      alert.showErrorToast(context, 'Meeting could not be created', 'Meeting Creation Failed');
    } finally {
      setState(() => _isSubmitting = false);
    }
  }

  @override
  void dispose() {
    _titleController.dispose();
    _descriptionController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Create Meeting'),
      drawer: SideNavDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              TextFormField(
                controller: _titleController,
                decoration: const InputDecoration(labelText: 'Title'),
                validator: (value) => value == null || value.isEmpty
                    ? 'Please enter a title'
                    : null,
              ),
              TextFormField(
                controller: _descriptionController,
                decoration: const InputDecoration(labelText: 'Description'),
              ),
              const SizedBox(height: 24),
              _isSubmitting
                  ? const CircularProgressIndicator()
                  : ElevatedButton(
                onPressed: _submit,
                child: const Text('Create Meeting'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}