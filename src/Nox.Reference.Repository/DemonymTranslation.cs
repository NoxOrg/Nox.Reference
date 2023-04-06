using System;
using System.Collections.Generic;

namespace Nox.Reference.Repository;

public partial class DemonymTranslation
{
    public short DemonymId { get; set; }

    public short LanguageId { get; set; }

    public string Feminine { get; set; } = null!;

    public string Masculine { get; set; } = null!;

    public virtual Demonym Demonym { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
