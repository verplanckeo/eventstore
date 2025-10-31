using System;
using System.Collections.Generic;
using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.TimeEntry.DomainEvents;

namespace EventStore.Core.Domains.TimeEntry;

public class TimeEntry : EventSourcedAggregateRoot<TimeEntryId>
    {
        public override TimeEntryId Id { get; protected set; }
        public DateTimeOffset From { get; private set; }
        public DateTimeOffset Until { get; private set; }
        public string UserId { get; private set; }
        public string ProjectId { get; private set; }
        public ActivityTypes ActivityType { get; private set; }
        public string Comment { get; private set; }
        
        public bool IsRemoved { get; private set; }

        //TODO: Rework default constructor - for now it's only used for unit test
        /// <summary>
        /// DO NOT USE THIS CTOR!
        /// </summary>
        public TimeEntry() { }

        
        /// <summary>
        /// When an aggregate has been fetched from db, we call this CTor which will apply all events and increase the Version by 1
        /// </summary>
        /// <param name="events"></param>
        public TimeEntry(IEnumerable<IDomainEvent> events) : base(events) { }

        public static TimeEntry RegisterTimeEntry(
            TimeEntryId id,
            DateTimeOffset from,
            DateTimeOffset until,
            string userId,
            string projectId,
            ActivityTypes activityType,
            string comment)
        {
            ValidateTimeEntryInputs(from, until, userId, projectId, comment);

            var timeEntry = new TimeEntry();
            
            var @event = TimeEntryRegisteredDomainEvent.CreateEvent(
                id.ToString(),
                from,
                until,
                userId,
                projectId,
                activityType,
                comment ?? string.Empty);

            timeEntry.Apply(@event);
            return timeEntry;
        }

        public void ChangeTimeEntry(
            string aggregateRootId,
            DateTimeOffset from,
            DateTimeOffset until,
            string userId,
            string projectId,
            ActivityTypes activityType,
            string comment)
        {
            ValidateTimeEntryInputs(from, until, userId, projectId, comment);

            var @event = TimeEntryChangedDomainEvent.CreateEvent(
                aggregateRootId,
                from,
                until,
                userId,
                projectId,
                activityType,
                comment ?? string.Empty);

            Apply(@event);
        }
        private static void ValidateTimeEntryInputs(
            DateTimeOffset from,
            DateTimeOffset until,
            string userId,
            string projectId,
            string comment)
        {
            if (from >= until)
                throw new ArgumentException("From date must be before Until date.");

            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.");

            if (string.IsNullOrWhiteSpace(projectId))
                throw new ArgumentException("ProjectId is required.");

            if (!string.IsNullOrWhiteSpace(comment) && comment.Length > 100)
                throw new ArgumentException("Comment cannot exceed 100 characters.");
        }

        public void RemoveTimeEntry()
        {
            var @event = TimeEntryRemovedDomainEvent.CreateEvent();
            Apply(@event);
        }

        public void On(TimeEntryRegisteredDomainEvent @event)
        {
            Id = new TimeEntryId(@event.TimeEntryId);
            From = @event.From;
            Until = @event.Until;
            UserId = @event.UserId;
            ProjectId = @event.ProjectId;
            ActivityType = @event.ActivityType;
            Comment = @event.Comment;
        }

        public void On(TimeEntryChangedDomainEvent @event)
        {
            From = @event.From;
            Until = @event.Until;
            UserId = @event.UserId;
            ProjectId = @event.ProjectId;
            ActivityType = @event.ActivityType;
            Comment = @event.Comment;
        }

        public void On(TimeEntryRemovedDomainEvent @event)
        {
            IsRemoved = true;
        }
    }