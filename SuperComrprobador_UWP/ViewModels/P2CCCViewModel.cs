using System;
using Microsoft.UI.Xaml;
using SuperComrprobador_UWP.Helpers;

namespace SuperComrprobador_UWP.ViewModels
{
    public class P2CCCViewModel : Observable
    {
        private string _banco = string.Empty;
        public string Banco
        {
            get => _banco;
            set { Set(ref _banco, value); Validar(); }
        }

        private string _oficina = string.Empty;
        public string Oficina
        {
            get => _oficina;
            set { Set(ref _oficina, value); Validar(); }
        }

        private string _dc = string.Empty;
        public string DC
        {
            get => _dc;
            set { Set(ref _dc, value); Validar(); }
        }

        private string _cuenta = string.Empty;
        public string Cuenta
        {
            get => _cuenta;
            set { Set(ref _cuenta, value); Validar(); }
        }

        private string _resultado = string.Empty;
        public string Resultado
        {
            get => _resultado;
            private set => Set(ref _resultado, value);
        }

        private bool _esValido;
        public bool EsValido
        {
            get => _esValido;
            private set => Set(ref _esValido, value);
        }

        private bool _hayResultado;
        public bool HayResultado
        {
            get => _hayResultado;
            private set
            {
                Set(ref _hayResultado, value);
                OnPropertyChanged(nameof(ResultadoVisibility));
            }
        }

        public Visibility ResultadoVisibility => _hayResultado ? Visibility.Visible : Visibility.Collapsed;

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(_banco) && string.IsNullOrWhiteSpace(_oficina) &&
                string.IsNullOrWhiteSpace(_dc) && string.IsNullOrWhiteSpace(_cuenta))
            {
                HayResultado = false;
                return;
            }

            if (_banco.Length != 4 || _oficina.Length != 4 || _dc.Length != 2 || _cuenta.Length != 10)
            {
                HayResultado = true;
                EsValido = false;
                Resultado = "✘ Formato: Banco(4) + Oficina(4) + DC(2) + Cuenta(10), todos numéricos";
                return;
            }

            if (!long.TryParse(_banco, out _) || !long.TryParse(_oficina, out _) ||
                !long.TryParse(_dc, out _) || !long.TryParse(_cuenta, out _))
            {
                HayResultado = true;
                EsValido = false;
                Resultado = "✘ Todos los campos deben ser numéricos";
                return;
            }

            HayResultado = true;
            string dcCalculado = CheckCCC.CheckCCC.CheckDC(_banco, _oficina, _cuenta);
            EsValido = dcCalculado == _dc;
            Resultado = EsValido
                ? $"✔ CCC válido. Dígitos de control correctos: {dcCalculado}"
                : $"✘ CCC inválido. Los dígitos de control correctos son '{dcCalculado}', no '{_dc}'";
        }
    }
}
