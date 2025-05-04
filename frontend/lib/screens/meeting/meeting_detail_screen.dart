import 'package:flutter/material.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/services/meeting_api_service.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/services/meeting_api_service.dart';

class MeetingDetailScreen extends StatelessWidget {
  final int meetingId;
  const MeetingDetailScreen({Key? key, required this.meetingId}) : super(key: key);
  static const String id = "meeting_detail_screen";
  @override
  Widget build(BuildContext context) {
    final MeetingAPIService _service = MeetingAPIService();

    return Scaffold(
      appBar: AppBar(
        title: const Text(
          "CRM: Meeting Details",
          textAlign: TextAlign.center,
          style: TextStyle(
            fontFamily: "Ubuntu-Bold",
            fontSize: 24.0,
            fontWeight: FontWeight.w700,
            color: Colors.white,
          ),
        ),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<UserMeeting>(
        future: _service.getMeetingById(meetingId),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting)
            return const Center(child: CircularProgressIndicator());
          if (snapshot.hasError)
            return Center(child: Text('Error: ${snapshot.error}'));

          final meeting = snapshot.data!;
          return Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text('Meeting: ${meeting.meetingId}', style: const TextStyle(fontSize: 18)),
                const SizedBox(height: 8),
                Text('Date: ${meeting.meeting!.dateCreated}'),
                const SizedBox(height: 8),
                Text('Description: ${meeting.meeting!.description ?? "No description"}'),
              ],
            ),
          );
        },
      ),
    );
  }
}