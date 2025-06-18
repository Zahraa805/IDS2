using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace IDS
{
    public class MyAmazingNotesToRemember
    {

        /////////////Important notes About null
        #region
        ////////////////Important about null : 
        // var tickets = _context.Tickets; // ✅ This is an IQueryable<Ticket>, not a list! This does not retrieve any data immediately.    
        //  The query only executes when you iterate over it(e.g., using .ToList(), .FirstOrDefault(), etc.).

        //ToList() returns a list(empty or filled)
        //FirstOrDefault() returns a single object(or null if nothing is found)


        // What Happens When There’s No Match?
        //Query                                                         Return Type                                            What It Returns If No Match?
        //_context.Tickets.Where(e => e.Id == id);                      IQueryable<Ticket>                                     An empty queryable(not null)
        //_context.Tickets.Where(e => e.Id == id).ToList();             List<Ticket>                                           An empty list[]

        //_context.Tickets.Select(e => e.Id);                           IQueryable<int>                                        Empty queryable(not null)
        //_context.Tickets.Select(e => e.Id).ToList();                  List<int> Empty                                        list[](not null)

        //_context.Tickets.FirstOrDefault(e => e.Id == id);             Ticket ?                                                (nullable)null
        //_context.Tickets.SingleOrDefault(e => e.Id == id);            Ticket ?                                                (nullable)null(if no match), Exception(if multiple matches)

        #endregion



        #region
        //,, important about db ;loading , queries and data reader
        //  .ToList() retrieves all tickets from the database and loads them into memory.
        // .FirstOrDefault(t => t.TicketId == id) filters the list in memory to find the first matching record



        // ✅ Fix: Remove.ToList() and use FirstOrDefaultAsync() instead.
        // When you use.ToList(), EF keeps the DataReader open while it loads all records.
        //If your database has 100,000 tickets, all of them are loaded into memory even though you only need one!
        //This wastes memory and slows down performance.
        //It increases database load and can lead to higher RAM usage in large systems.

        //Missing await for database operations
        //Since you're inside an async action, database queries should use await with FirstOrDefaultAsync() to avoid blocking the thread.
        #endregion


        // Wrong code 
        //var ticket = _context.Tickets
        //.AsNoTracking()

        //.Include(t => t.MedicalHistory)
        //.Include(t => t.Patient)
        //.Include(t => t.ReferredTo)
        //.ToList()
        //.FirstOrDefault(t => t.TicketId == id); // ✅ Use ToList()

        //var ticket = await _context.Tickets
        //    .AsNoTracking()
        //    .Include(t => t.MedicalHistory)
        //    .Include(t => t.Patient)
        //    .Include(t => t.ReferredTo)
        //    .FirstOrDefaultAsync(t => t.TicketId == id); // ✅ Use FirstOrDefaultAsync
    }




    //    <script>
    //    function loadPartialView(url)
    //{
    //        $.ajax({
    //    url: url,  // Send request to the given URL
    //            type: "GET",  // Use GET method
    //            success: function(data) {
    //                $("#partialView").html(data);  // Insert response (HTML) into the placeholder
    //        },
    //            error: function() {
    //            alert("Error loading content.");  // Show an error message if something goes wrong
    //        }
    //    });
    //}
    //</script>

    //<script>
    //    //  you've already created the layout and the mechanism for loading partial views 
    //    // dynamically via JavaScript (AJAX). 
    //    // Add this JavaScript block to load the correct partial view automatically when redirected:
    //    //  You already have loadPartialView(url) working — this just calls it when the page loads.
    //    window.onload = function()
    //{
    //    const urlParams = new URLSearchParams(window.location.search);
    //    const load = urlParams.get('load');
    //    if (load)
    //    {
    //        loadPartialView('/' + load);
    //    }
    //};
    //</script>
}
