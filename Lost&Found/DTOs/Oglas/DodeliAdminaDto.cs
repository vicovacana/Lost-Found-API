namespace Lost_Found.DTOs.Oglas
{
    public class DodeliAdminaDto
    {
        // Omitted/null means "assign myself" (handled in the controller). To clear an
        // assignment entirely, use DELETE /api/oglasi/{id}/admin instead.
        public int? AdminId { get; set; }
    }
}
