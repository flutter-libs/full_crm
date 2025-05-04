import 'package:flutter/material.dart';
import 'package:frontend/models/user.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/services/meeting_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class AddParticipantsScreen extends StatefulWidget {
  final int? meetingId;
  final List<User>? allUsers; // This should be passed in from the parent screen
  const AddParticipantsScreen({
    super.key,
    this.meetingId,
    this.allUsers,
  });
  static const String id = "add_participants_screen";
  @override
  State<AddParticipantsScreen> createState() => _AddParticipantsScreenState();
}

class _AddParticipantsScreenState extends State<AddParticipantsScreen> {
  final MeetingAPIService _meetingService = MeetingAPIService();
  final Set<String> _selectedUserIds = {};
  bool _isSubmitting = false;

  void _toggleUserSelection(String userId) {
    setState(() {
      if (_selectedUserIds.contains(userId)) {
        _selectedUserIds.remove(userId);
      } else {
        _selectedUserIds.add(userId);
      }
    });
  }

  Future<void> _submitParticipants() async {
    if (_selectedUserIds.isEmpty) {
      alert.showInfoToast(context, 'Please select at least one user', 'Select A User');
      return;
    }

    setState(() {
      _isSubmitting = true;
    });

    try {
      bool success = await _meetingService.addParticipants(
        widget.meetingId!,
        _selectedUserIds.toList(),
      );

      if (success) {
        alert.showSuccessToast(context, 'Added user to the meeting', 'Added User');
        Navigator.pop(context, true);
      } else {
        alert.showErrorToast(context, 'Server returned false', 'Server Error');
      }
    } catch (e) {
      alert.showErrorToast(context, 'There has been an Exception: $e', 'Error Exception');
    } finally {
      setState(() {
        _isSubmitting = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Add Participants', showBackButton: false),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: widget.allUsers!.length,
              itemBuilder: (context, index) {
                final user = widget.allUsers![index];
                final isSelected = _selectedUserIds.contains(user.id);
                return CheckboxListTile(
                  title: Text(user.name ?? user.userName ?? 'Unknown'),
                  subtitle: Text(user.email ?? ''),
                  value: isSelected,
                  onChanged: (_) => _toggleUserSelection(user.id!),
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: _isSubmitting
                ? const CircularProgressIndicator()
                : ElevatedButton(
              onPressed: _submitParticipants,
              child: const Text('Add Selected Participants'),
            ),
          ),
        ],
      ),
    );
  }
}