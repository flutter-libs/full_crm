import 'package:flutter/material.dart';
import 'package:frontend/screens/lead/lead_update_screen.dart';
import 'package:frontend/services/lead_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:intl/intl.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;

class LeadDetailScreen extends StatelessWidget {
  final Lead? lead;
  final int? leadId;
  static const String id = "lead_detail_screen";
  const LeadDetailScreen({super.key, this.leadId, this.lead});
  Future<void> _deleteLead(BuildContext context, int leadId) async {
    final confirm = await showDialog<bool>(
      context: context,
      builder: (ctx) => AlertDialog(
        title: Text("Confirm Delete"),
        content: Text("Are you sure you want to delete this lead?"),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(ctx).pop(false),
            child: Text("Cancel"),
          ),
          ElevatedButton(
            onPressed: () => Navigator.of(ctx).pop(true),
            child: Text("Delete"),
          ),
        ],
      ),
    );


    if (confirm == true) {
      await LeadApiService().deleteLead(leadId);
      alert.showSuccessToast(context, 'Lead deleted', 'Deleted Lead');
      Navigator.of(context).pop(); // or pushReplacementNamed to update the list
    } else {
      alert.showErrorToast(context, 'Lead was not deleted', 'Deletion Failed');
      Navigator.of(context).pop();
    }
  }

  void editLead(BuildContext context, int leadId) {
    Navigator.pushNamed(context, LeadUpdateScreen.id);
  }
  @override
  Widget build(BuildContext context) {
    final dateFormat = DateFormat('yyyy-MM-dd HH:mm');

    return Scaffold(
      appBar: CustomAppBar(
        title: lead?.leadName ?? 'Lead Details',
        actions: [
          IconButton(
            icon: Icon(Icons.edit),
            onPressed: () {
              Navigator.pushNamed(context, LeadUpdateScreen.id);
            },
          ),
          IconButton(
            icon: Icon(Icons.delete),
            onPressed: () {
              _deleteLead(context, leadId!);
            },
          ),
        ],
        showBackButton: false,
      ),
      drawer: SideNavDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            detailRow("Name", lead?.leadName),
            detailRow("Email", lead?.leadEmail),
            detailRow("Phone", lead?.leadPhone),
            detailRow("Fax", lead?.leadFax),
            detailRow("Website", lead?.leadWebsite),
            detailRow("Address", lead?.leadAddress),
            detailRow("City", lead?.leadCity),
            detailRow("State", lead?.leadState),
            detailRow("Zip", lead?.leadZip),
            detailRow("Country", lead?.leadCountry),
            detailRow("Notes", lead?.leadNotes),
            detailRow("Created", lead?.created != null ? dateFormat.format(lead!.created!) : null),
            detailRow("Updated", lead?.updated != null ? dateFormat.format(lead!.updated!) : null),
            detailRow("Created By", lead?.createdByUser?.userName ?? lead?.createdBy),
          ],
        ),
      ),
    );
  }

  Widget detailRow(String label, String? value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: value == null
          ? SizedBox.shrink()
          : Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            "$label: ",
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          Expanded(child: Text(value)),
        ],
      ),
    );
  }
}