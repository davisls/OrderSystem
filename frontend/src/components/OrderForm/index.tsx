import { useState } from "react";
import { api } from "../../services/api";
import "./styles.css";

interface Props {
  onCreated: () => void;
}

export function OrderForm({ onCreated }: Props) {
  const [customer, setCustomer] = useState("");
  const [amount, setAmount] = useState("");
  const [orderDate, setOrderDate] = useState("");

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    await api.post("/orders", {
      customer,
      value: Number(amount),
      orderDate: new Date(orderDate),
    });

    setCustomer("");
    setAmount("");
    setOrderDate("");

    onCreated();
  }

  return (
    <form className="order-form" onSubmit={handleSubmit}>
      <h2>Novo Pedido</h2>

      <input
        type="text"
        placeholder="Cliente"
        value={customer}
        onChange={(e) => setCustomer(e.target.value)}
      />

      <input
        type="number"
        placeholder="Valor"
        value={amount}
        onChange={(e) => setAmount(e.target.value)}
      />

      <input
        type="date"
        placeholder="Data do Pedido"
        value={orderDate}
        onChange={(e) => setOrderDate(e.target.value)}
      />

      <button type="submit">Criar Pedido</button>
    </form>
  );
}
