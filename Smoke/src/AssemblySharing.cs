using System.Runtime.CompilerServices;

// If the namespace of the thing using this library
// says that it's the runner or engine then allow
// internal properties to be shared with it. This
// lets us hide engine runtime specific data from
// little timmy whos using smoke for the first time
[assembly: InternalsVisibleTo("Runner")]
[assembly: InternalsVisibleTo("Engine")]