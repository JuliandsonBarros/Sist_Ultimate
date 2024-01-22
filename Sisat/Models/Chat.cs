using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sisat.Models
{
    [Table("Mensagens")]
    public class Chat
    {
        [Key]
        [Column("Id_Mensagem")]
        public int IdMensagem { get; set; }
        [Column("Id_Remetente")]
        public int IdRemetente { get; set; }
        [Column("Id_Destinatario")]
        public int IdDestinatario { get; set; }
        [Column("Remetente_tipo")]
        public int RemetenteTipo { get; set; }
        [Column("Titulo")]
        public string? Titulo { get; set; }
        [Column("Mensagem")]
        public string? Mensagem { get; set; } = null!;
        [Column("Resposta")]
        public string? Resposta { get; set; } = null!;
        [Column("Data_Envio", TypeName = "datetime")]
        public DateTime? DataEnvio { get; set; }
        [Column("visualizacao")]
        public bool Visualizacao { get; set; }

        [ForeignKey(nameof(IdDestinatario))]
        [InverseProperty(nameof(Usuario.ChatIdDestinatarioNavigation))]
        public virtual Usuario IdDestinatarioNavigation { get; set; } = null!;
        [ForeignKey(nameof(IdRemetente))]
        [InverseProperty(nameof(Usuario.ChatIdRemetenteNavigation))]
        public virtual Usuario IdRemetenteNavigation { get; set; } = null!;

    }
}
