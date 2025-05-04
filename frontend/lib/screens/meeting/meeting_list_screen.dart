import 'package:flutter/material.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/services/meeting_api_service.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/services/meeting_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class MeetingListScreen extends StatefulWidget {
  const MeetingListScreen({super.key});
  static const String id = "meeting_list_screen";
  @override
  State<MeetingListScreen> createState() => _MeetingListScreenState();
}

class _MeetingListScreenState extends State<MeetingListScreen> {
  final MeetingAPIService _meetingService = MeetingAPIService();
  late Future<List<UserMeeting>> _futureMeetings;

  @override
  void initState() {
    super.initState();
    _futureMeetings = _meetingService.getMeetings();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Meeting List', showBackButton: false),
      body: FutureBuilder<List<UserMeeting>>(
        future: _futureMeetings,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return const Center(child: Text('No meetings found.'));
          } else {
            final meetings = snapshot.data!;
            return ListView.builder(
              itemCount: meetings.length,
              itemBuilder: (context, index) {
                final meeting = meetings[index];
                return ListTile(
                  title: Text(meeting.meeting!.title ?? 'Untitled Meeting'),
                  subtitle: Text('Date: ${meeting.meeting!.dateCreated}'),
                  trailing: IconButton(
                    onPressed: (){
                      () async {
                        final confirm = await showDialog<bool>(
                            context: context,
                            builder: (context) => AlertDialog(
                              title: const Text('Delete Meeting'),
                              content: const Text('Are you sure you want to delete this meeting?'),
                              actions: [
                                TextButton(onPressed: () => Navigator.pop(context, false), child: const Text('Cancel')),
                                ElevatedButton(onPressed: () => Navigator.pop(context, true), child: const Text('Delete')),
                              ],
                            )
                        );
                        if (confirm == true) {
                          try {
                            final success = await _meetingService.deleteMeeting(meeting!.id!);
                            if (success) {
                              alert.showSuccessToast(context, 'Success deleting meeting', 'Success');
                              setState(() {
                                _futureMeetings = _meetingService.getMeetings();
                              });
                            } else {
                              alert.showErrorToast(context, 'Deleting failed, please try another method, or contact IT', 'Deleting Failed');
                            }
                          } catch (e) {
                            alert.showErrorToast(context, 'There was an Exception: $e on meeting_list_screen', 'Error');
                          }
                        }
                    };
                    },
                    icon: Icon(Icons.delete, color: Colors.red,),
                  ),
                  onTap: () {
                    // Add navigation to view/edit if needed
                  },
                );
              },
            );
          }
        },
      ),
    );
  }
}