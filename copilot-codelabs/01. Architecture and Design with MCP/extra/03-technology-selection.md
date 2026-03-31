# Exercise 3: Technology Selection Based on Architecture

## ⏱️ Time: 15 minutes

---

## 🎯 Goal

Use Copilot to select specific technologies for each component of your architecture.

---

## 📖 Context

You now have:
1. ✅ Requirements understood (Exercise 1)
2. ✅ Architecture designed (Exercise 2)

Now you need to **choose concrete technologies**:
- Which database? (PostgreSQL, MySQL, MongoDB?)
- Which API framework? (Express, FastAPI, ASP.NET Core?)
- Which hosting? (Heroku, AWS, Docker?)
- Which email/SMS providers? (SendGrid, Twilio?)

**Key principle:** Your technology choices should be driven by your requirements and architecture, not by hype or personal preference.

---

## 📋 Tasks

### Task 0 (Optional): Create Technology Evaluation Instructions

Create `.tech-selection-instructions.md` to standardize technology decisions:

```markdown
# Technology Selection Instructions

You are a technology architect evaluating tools and frameworks.

1. **For each technology comparison, always provide**:
   - Name of 3-4 options
   - Clear pros/cons for each
   - Cost comparison (if applicable)
   - Team expertise consideration
   - Recommendation with justified reasoning

2. **Evaluation criteria to always consider**:
   - Cost (licensing, infrastructure, operational)
   - Performance characteristics
   - Learning curve / team impact
   - Community support / maturity
   - Operational complexity
   - Scalability limits
   - Integration with existing systems

3. **Format output as**:
   - Comparison table (for quick overview)
   - Detailed analysis per option (1-2 paragraphs each)
   - Clear recommendation section
   - Alternative options if constraints change

4. **Always include trade-offs**:
   - What are we gaining?
   - What are we losing?
   - When would we reconsider this choice?

5. **Final tech stack section must include**:
   - Component name
   - Technology choice
   - Reasoning
   - Risk assessment
```

**Pro Tip:** This instruction file ensures technology decisions are always justified, not arbitrary!

---

### Task 1: Evaluate Database Options

Ask Copilot:

```
Using my .tech-selection-instructions.md guidelines:

I'm building an appointment booking system with these requirements:
- 1,000 bookings/day
- Strong consistency needed (no double-booking)
- 5 years data retention
- Simple data model (Owners, Pets, Vets, Appointments)

Compare these database options:
1. PostgreSQL (open source, SQL)
2. MySQL (open source, SQL)
3. MongoDB (NoSQL, document-based)

For each:
- Pros and cons
- Cost (license, infrastructure)
- Operational complexity
- Fit for our requirements

Which one would you recommend and why?
```

**What to expect:**
Copilot will structure its comparison using your instruction framework.

---

### Task 2: Evaluate API Framework Options

Ask Copilot:

```
I need to build a REST API for appointment booking. The API needs to:
- Handle 500 concurrent users
- Response time < 3 seconds for booking
- Easy to test and deploy
- Good for team of 3-4 developers

Compare these frameworks:
1. Node.js + Express (JavaScript)
2. Python + FastAPI
3. .NET Core (C#)
4. Spring Boot (Java)

For each:
- Learning curve for team
- Performance characteristics
- Community support
- Operational complexity

Which would you recommend for a small team and why?
```

**What to expect:**
- Comparison of frameworks
- Recommendation based on team skills
- Trade-offs explained

---

### Task 3: Evaluate Email & SMS Providers

Ask Copilot:

```
I need to send appointment confirmation emails and SMS messages.

Compare these options for email:
1. SendGrid
2. Mailgun
3. Amazon SES

Compare these options for SMS:
1. Twilio
2. AWS SNS
3. Nexmo (Vonage)

For each provider, consider:
- Cost per email/SMS
- Delivery reliability
- Integration ease
- Support quality
- Scaling limitations

What would you recommend?
```

**What to expect:**
- Pricing comparison
- Feature comparison
- Recommendation

---

### Task 4: Create Complete Tech Stack Document

Ask Copilot:

```
Based on my previous questions and recommendations, create a complete technology stack decision document.

Include:

1. **Frontend**:
   - Technology choice (React, Vue, etc.)
   - Hosting (GitHub Pages, Netlify, etc.)
   - Why this choice

2. **API**:
   - Framework (Express, FastAPI, etc.)
   - Language
   - Why this choice

3. **Database**:
   - Technology (PostgreSQL, MySQL, etc.)
   - Why this choice
   - Backup strategy

4. **Notifications**:
   - Email provider (SendGrid, etc.)
   - SMS provider (Twilio, etc.)
   - Why these choices

5. **Deployment**:
   - Container (Docker?)
   - Hosting (AWS, Heroku, DigitalOcean?)
   - CI/CD (GitHub Actions, GitLab CI?)
   - Why this choice

6. **Cost Estimate**:
   - Monthly costs for each service
   - Total estimated monthly cost
   - How to optimize if needed

Format as a structured decision document.
```

**What to expect:**
- Complete tech stack document
- Cost breakdown
- Justifications for each choice

---

## 💡 Sample Tech Stack Output

Here's what a good decision document might look like:

```markdown
# Technology Stack Decision

## Frontend
- **Choice**: React
- **Hosting**: Vercel (free tier + paid)
- **Why**: Fast build, good for SPA, easy deployment

## API
- **Choice**: Python + FastAPI
- **Why**: Small team can learn quickly, great documentation
- **Deployment**: Docker container on AWS

## Database
- **Choice**: PostgreSQL
- **Why**: ACID compliance prevents double-booking, proven reliability
- **Backup**: Daily automated backups to S3

## Notifications
- **Email**: SendGrid (free tier 100/day)
- **SMS**: Twilio ($0.01 per SMS)
- **Why**: Reliable, affordable, easy to integrate

## Deployment
- **Containerization**: Docker
- **Hosting**: AWS ECS (or Heroku for simplicity)
- **CI/CD**: GitHub Actions
- **Why**: Free CI/CD, standard practices, easy to scale

## Cost Estimate
- SendGrid: $25/month (free tier sufficient initially)
- Twilio: $50/month (estimated volume)
- AWS: $100/month (server, database, storage)
- Domain: $12/year
- **Total**: ~$175/month
```

---

## 📊 Deliverables

Save your outputs to `outputs/exercise-3-tech-stack.md`:

```markdown
# Exercise 3: Technology Selection

## Database Decision
[Comparison and recommendation]

## API Framework Decision
[Comparison and recommendation]

## Notification Providers Decision
[Email and SMS provider choices with justification]

## Complete Tech Stack
[Full technology stack document]

## Risk Assessment
[What could go wrong with these choices?]

## Alternate Stack (If Budget Changes)
[What if budget was reduced? What would change?]
```

---

## 💡 Best Practices: Using Instructions for Systematic Decisions

**Why instructions improve technology selection:**
- ✅ Ensures all options are fairly evaluated
- ✅ Forces justification (no hype-driven choices)
- ✅ Consistent criteria across all decisions
- ✅ Trade-offs explicitly documented
- ✅ Decision is defensible to stakeholders
- ✅ Easy to revisit later

**Instruction File Power:**
Instead of getting varied responses, you get consistent, structured evaluation every time!

---

## ✅ Success Criteria

Your tech selection succeeds if:

- [ ] You've evaluated at least 3 options for each major component
- [ ] You have a clear recommendation with justification
- [ ] You understand the tradeoffs (cost vs. performance vs. simplicity)
- [ ] Your choices align with requirements (not just hype)
- [ ] You have a rough cost estimate
- [ ] You can explain why to your team
- [ ] Did you create .tech-selection-instructions.md? (Recommended!)

---

## 🔍 Review Questions

After completing this exercise, ask yourself:

1. **Trade-off Analysis**: What are we gaining/losing with each choice?
2. **Risk**: What could fail with this stack?
3. **Team Fit**: Can our team support these choices?
4. **Cost**: Is this sustainable long-term?
5. **Flexibility**: Could we swap technologies later if needed?

---

## 🎓 What You're Learning

✅ How to evaluate technology options systematically
✅ How to consider cost in architecture decisions
✅ How to align tech choices with requirements
✅ How to document decisions (for future reference)
✅ How to think through trade-offs

---

## 🎉 Course Complete!

Congratulations! You've learned how to use **Copilot + MCP** for:

1. **Requirements Analysis** (Exercise 1)
2. **Architecture Design** (Exercise 2)
3. **Technology Selection** (Exercise 3)

### What You Did:

```
Requirements Document
    ↓ (Exercise 1: Analysis)
Understand What to Build
    ↓ (Exercise 2: Architecture)
Design How to Build It
    ↓ (Exercise 3: Tech Stack)
Choose With What to Build It
```

### Key Insights:

✅ MCP lets Copilot read your files (requirements, docs, code)
✅ Clear prompts get better responses
✅ Architecture should come BEFORE technology decisions
✅ Technology choices should be JUSTIFIED, not arbitrary

---

## 🚀 Next Steps (After Course)

What you can do with these new skills:

1. **Build the system**: Use tech stack to implement
2. **Design other systems**: Apply same process to other projects
3. **Improve processes**: Use Copilot + MCP for requirements, design, code review, testing
4. **Teach others**: Explain this approach to your team

---

## 📚 Resources

- [Copilot Documentation](https://github.com/features/copilot)
- [MCP (Model Context Protocol)](https://modelcontextprotocol.io/)
- [Architecture Decision Records (ADRs)](https://adr.github.io/)
- [System Design Interview Guide](https://github.com/donnemartin/system-design-primer)

---

**Well done! You now know how to design systems with Copilot.** 🎓
