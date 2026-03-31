# Exercise 4: Read Requirements from JIRA Using MCP

## ⏱️ Time: 25 minutes

---

## 🎯 Goal

Integrate JIRA with Copilot using MCP to read real requirements directly from JIRA issues and generate architecture based on live data.

---

## 📖 Context

So far, you've been analyzing **static local files** for requirements.

In a real project, requirements live in **JIRA**, and teams want to work directly with live data. This exercise teaches you how to:

1. **Connect JIRA to Copilot** using MCP (Model Context Protocol)
2. **Read requirements directly from JIRA issues** without exporting
3. **Generate architecture based on live requirements**
4. **Keep requirements and architecture in sync**

---

## 🔧 Setup: Enable JIRA MCP in VS Code

### Step 1: Install JIRA MCP Integration

The GitHub Copilot extension supports JIRA MCP out of the box. You need to:

1. **Get JIRA Cloud URL**: Find your JIRA instance URL
   - Example: `https://your-company.atlassian.net`

2. **Generate JIRA API Token**:
   - Go to: https://id.atlassian.com/manage-profile/security/api-tokens
   - Click "Create API token"
   - Save the token (you'll need it)

3. **Configure MCP Settings in VS Code**:
   - Open VS Code Settings (Cmd+,)
   - Search for "MCP"
   - Look for "GitHub Copilot" settings
   - Add JIRA MCP server configuration:

   ```json
   {
     "github.copilot.extensions.atlassian": {
       "enabled": true,
       "cloudId": "your-cloud-id",
       "email": "your-email@company.com",
       "token": "your-api-token"
     }
   }
   ```

4. **Verify Connection**:
   - Open Copilot chat
   - Try asking: "What's in JIRA project PET?"
   - Copilot should list JIRA issues

---

## 📋 Tasks

### Task 1: Discover JIRA Issues for Pet Appointment Booking

**Scenario**: Your JIRA project `PET` contains the appointment booking requirements that Exercises 1-3 analyzed locally.

In Copilot chat, ask:

```
List all JIRA issues in project PET that relate to appointment booking.

For each issue, show:
- Issue key + title
- Status
- Current story points
- Assigned person

Format as a table.
```

**What to expect:**
- A list of JIRA issues related to appointment booking
- Issue statuses (backlog, in-progress, done, etc.)
- Team assignments

**Example Output:**
```
| Key | Title | Status | Points |
|-----|-------|--------|--------|
| PET-1 | Owner books appointment | Backlog | 8 |
| PET-2 | Vet sees schedule | Backlog | 5 |
| PET-3 | Email confirmation | Backlog | 3 |
| PET-4 | Prevent double-booking | Backlog | 8 |
```

---

### Task 2: Read Full Requirements from JIRA Issues

Now ask Copilot to extract complete requirements from JIRA:

```
Read issues PET-1, PET-2, PET-3, PET-4 from JIRA.

For each issue, extract:
1. **User Story**: The main narrative (As a... I want... So that...)
2. **Acceptance Criteria**: The conditions for "done"
3. **Dependencies**: What other issues depend on this?
4. **Technical Notes**: Any technical constraints mentioned?

Organize as:

## Issue: [Key] - [Title]
User Story: ...
Acceptance Criteria:
- ...
- ...
Dependencies: ...
Technical Notes: ...
```

**What to expect:**
- Complete user stories as written in JIRA
- Acceptance criteria for each story
- Dependencies between issues
- Technical constraints

---

### Task 3: Generate Architecture from JIRA Requirements

This is the key value of MCP: **Generate architecture based on live JIRA data**.

Ask:

```
Based on the JIRA requirements you just read (PET-1 through PET-4):

1. **Identify the main components** needed to satisfy all issues
2. **Generate architecture diagram** (Mermaid)
3. **List API endpoints** needed
4. **Show data flow** for the booking process

Use my .architecture-instructions.md file to ensure consistent formatting.

Show your reasoning for each component - why is it needed based on JIRA issues?
```

**What to expect:**
- Architecture components justified by JIRA requirements
- Mermaid diagram with all services
- API endpoints referenced by acceptance criteria
- Clear traceability: "Issue PET-1 requires component X"

**Key Difference from Exercise 2:**
- Exercise 2: Analyzed static requirements.md file
- Exercise 4: Analyzes live JIRA issues + generates based on real acceptance criteria

---

### Task 4: Validate Requirements → Architecture Traceability

Create a traceability matrix showing that your architecture covers all JIRA requirements.

Ask Copilot:

```
Create a traceability matrix showing:

1. JIRA Issue (PET-1, PET-2, etc.)
2. Key Acceptance Criteria items
3. Architecture Components that address it
4. API endpoints that implement it

Format as:

| Issue | Acceptance Criteria | Component | API Endpoint |
|-------|-------------------|-----------|--------------|
| PET-1 | Owner searches slots | SlotService, Database | GET /slots |
| PET-1 | Owner sees availability | Frontend, SlotService | GET /slots |

This validates that every JIRA requirement is satisfied by our architecture.
```

**What to expect:**
- A matrix linking JIRA issues to architecture
- Confidence that nothing was missed
- Clear implementation path from requirements to architecture

---

### Task 5: Create a "Requirements Update" Scenario

Simulate a common scenario: **JIRA requirements change, and you need to update architecture**.

Ask Copilot:

```
Imagine we added a new JIRA issue:

PET-5: "Vet can reschedule appointments within 24 hours of booking"

Based on this new requirement:
1. What architecture components need to change?
2. What new API endpoints are needed?
3. What new data fields are required?
4. What's the impact on the system?

Show how this new requirement would fit into our existing architecture.
```

**What to expect:**
- Impact analysis of requirement changes
- Minimal architecture changes needed
- New endpoints: `PUT /appointments/{id}/reschedule`
- Data change: Add `last_rescheduled_at` field

**Real-World Value:**
This shows **how JIRA as a source of truth keeps architecture aligned with actual requirements**.

---

## 💡 Best Practices: Using JIRA with Copilot via MCP

### When to Use JIRA + Copilot + MCP

✅ **Great use cases:**
- Requirements are constantly changing
- Large project with many stakeholders
- Team collaborates in JIRA (existing workflow)
- Need real-time updates to architecture
- Traceability requirements (compliance, audits)

❌ **Poor use cases:**
- Simple one-time projects
- Requirements already stable
- No JIRA infrastructure
- Security concerns with API tokens

### Best Practices

**1. Keep JIRA as Source of Truth**
```
JIRA Issue (the truth)
    ↓ (Copilot reads)
Architecture Design
    ↓ (Generates)
Implementation Plan
    ↓ (Team executes)
Code
```

**2. Use Acceptance Criteria as Architecture Guide**
- Each acceptance criterion → API endpoint or component
- Cross-reference issue key in architecture doc
- When issue changes, architecture reasoning links back to it

**3. Version Your Architecture**
```markdown
# Architecture v1.2
Generated from JIRA issues as of 2024-03-24
Last synced: PET-1 through PET-5

## Traceability
- PET-1: SlotService, Frontend
- PET-2: ScheduleView, API
...
```

**4. Automate Sync where Possible**
- Weekly architecture reviews triggered by JIRA changes
- Alert if new issues added without architecture review
- Link JIRA issues to architecture branches/files

---

## 📊 Deliverables

Save all outputs to `outputs/exercise-4-jira-architecture.md`:

```markdown
# Exercise 4: Architecture from JIRA

## JIRA Issues Overview
[Table of issues, status, points]

## Requirements Extracted from JIRA
[User stories and acceptance criteria]

## Architecture Components
[Based on JIRA requirements]

## Architecture Diagram
```mermaid
[Your architecture]
```

## API Endpoints & JIRA Traceability
| Endpoint | JIRA Issue | Purpose |
|----------|-----------|---------|
| GET /slots | PET-1 | Owner searches available slots |
| POST /appointments | PET-1 | Owner books appointment |
...

## Integration Points with JIRA
[How does architecture support JIRA workflows?]

## Sync Status
Generated from: PET-1 through PET-5
Last updated: [Date]
Next sync: [Planned date]
```

---

## ✅ Success Criteria

Your JIRA + MCP integration succeeds if:

- [ ] You connected JIRA to Copilot via MCP
- [ ] You can read JIRA issues in Copilot chat
- [ ] You extracted all acceptance criteria from JIRA
- [ ] Your architecture is linked to specific JIRA issues
- [ ] You created a traceability matrix
- [ ] You validated that no requirements were missed
- [ ] You can show impact of requirement changes
- [ ] Architecture clearly states which JIRA version it's based on

---

## 🔍 Reflection Questions

After completing this exercise, consider:

1. **Efficiency**: Was it faster to read from JIRA vs. static file?
2. **Accuracy**: Did you miss any requirements? Why?
3. **Collaboration**: How would JIRA changes impact your architecture?
4. **Automation**: Could this workflow be automated for your team?
5. **Traceability**: Can someone trace from code → architecture → JIRA issue?

---

## 🎓 What You're Learning

✅ How MCP integrates external systems (JIRA) with Copilot
✅ How to work with live requirements instead of static documents
✅ How to maintain traceability from requirements → architecture → code
✅ How to handle evolving requirements gracefully
✅ Real-world workflow: JIRA is source of truth, Copilot is assistant
✅ Alternative to local analysis: Cloud-native collaboration

---

## 🚀 Advanced: Continuous Architecture Sync

Once you master this exercise, consider:

1. **Architecture as Code in JIRA**
   - Store architecture diagrams in JIRA
   - Update architecture when requirements change
   - Generate code stubs from architecture + JIRA

2. **Automated Validation**
   - Copilot validates: Does code match architecture?
   - Does architecture address all JIRA acceptance criteria?
   - Flag when JIRA changes but architecture doesn't update

3. **Team Workflow**
   - Architect: Creates architecture from JIRA (Exercise 4)
   - Developer: Implements based on architecture + JIRA
   - QA: Tests against acceptance criteria in JIRA
   - All tools point back to JIRA as source of truth

---

## 📚 Resources

- [JIRA Cloud API Docs](https://developer.atlassian.com/cloud/jira/platform/rest/v3/)
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [Model Context Protocol Specification](https://modelcontextprotocol.io/)
- [JIRA Atlassian API Tokens](https://support.atlassian.com/atlassian-account/docs/manage-api-tokens-for-your-atlassian-account/)

---

## 🎓 Course Summary

| Exercise | Focus | Source | Time |
|----------|-------|--------|------|
| 0 | Setup | - | 5 min |
| 1 | Understand Requirements | Local file | 15 min |
| 2 | Design Architecture | Local file | 20 min |
| 3 | Select Technology | Local context | 15 min |
| 4 | Live Data Integration | JIRA via MCP | 25 min |

**Total**: ~80 minutes to learn:
- How to work with Copilot for architecture
- How to create instruction files for consistency
- How to integrate live data sources (JIRA) via MCP
- How to maintain requirements → architecture traceability
